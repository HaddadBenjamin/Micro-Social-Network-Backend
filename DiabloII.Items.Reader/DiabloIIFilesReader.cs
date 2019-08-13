using DiabloII.Items.Reader.Extensions;
using DiabloII.Items.Reader.Records;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiabloII.Items.Reader
{
    public class DiabloIIFilesReader
    {
        private List<(string Name, string Type)> MissingItemTypes = new List<(string, string)>();
        private List<(string Name, string ItemName, double Id)> MissingSkills = new List<(string, string, double)>();
        private List<(string Name, string Property)> MissingProperties = new List<(string, string)>();
		private string CurrentItemName;
		// TODO : 
		// Les types h2h et 2h2 ne sont pas mapper, il semble manquer des Claw / Druid helm / barb helm/ necor shield / etc..
		// La partie avec  weaponSubCategoriesRecord.AddRange(new[]) : ne contient pas encore l'armure et les dommages et les stats requis, attack speed
		// Utiliser le nouveau document properties de Ascended pour calculer "IsPercent" et "Description" des attributs.
		// Class Skill et Gethit-Skill (skill when touched) PAR : min max, 
		// Gethit-Skill : 10% to trigger level 5 skill
		// Vérifier, tester et sanitizer les propriétés sur la vraie documentation
		// Ascended m'aide sur les missing category, il va me renvoyer lees documents weapon et armor txt.
		public IEnumerable<Item> Read(
            string uniquesCsv,
            string weaponsCsv,
            string armorsCsv,
			string propertiesCsv,
			string skillsCsv,
			string skillTabsCsv)
        {
            var weapons = ReadWeapons(weaponsCsv);
            var armors = ReadArmors(armorsCsv);
            var subCategories = ReadSubCategories(weapons, armors);
            var properties = ReadProperties(propertiesCsv);
			var skills = ReadSkills(skillsCsv);
			var skillTabs = ReadSkillTabs(skillTabsCsv);
			var uniques = ReadUniques(uniquesCsv, subCategories, properties, skills, skillTabs);

			// For sanitize purpoeses and comparaison :
			var sub = string.Join("\n- ", subCategories.Select(s => s.Name).OrderBy(x => x));
            var uni = string.Join("\n- ", MissingItemTypes.Distinct().OrderBy(x => x));
            var subCategoriesEnums = string.Join(",\n", subCategories.Select(s => s.SubCategory.Replace(" ", "_")).OrderBy(x => x).Distinct());
			var allProperties = string.Join(Environment.NewLine, uniques.SelectMany(_ => _.Properties).Select(_ => _.Name).Distinct().ToList());
			var missingProps = string.Join("\n- ", uniques.SelectMany(_ => _.Properties.Select(p => p.Name).Where(name => properties.FirstOrDefault(s => s.Name == name) == null).Distinct()).Distinct().ToList());
			var missingItemsCategories = string.Join("\n- ", MissingItemTypes.OrderBy(_ => _.Name).Distinct().Select(_ => $"{_.Name} : {_.Type}"));
            var missingProperties = string.Join("\n- ", MissingProperties.OrderBy(_ => _.Name).Distinct().Select(_ => $"{_.Name} : {_.Property}").Where(_ => !string.IsNullOrWhiteSpace(_)));
			var missingSkills = string.Join("\n- ", MissingSkills.OrderBy(_ => _.Name).Distinct().Select(_ => $"{_.ItemName} - {_.Name} : {_.Id}").Where(_ => !string.IsNullOrWhiteSpace(_)));

            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Missing Properties.txt"), missingProperties);
            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Missing Skills.txt"), missingSkills);
            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Missing Categories.txt"), missingItemsCategories);



            return uniques;
        }

        public IEnumerable<Item> ReadUniques(
			string uniquesCsv, 
			List<ItemCategoryRecord> itemCategories,
			List<PropertyRecord> propertyRecords,
			List<SkillRecord> skillRecords,
			List<SkillTabRecord> skillTabRecords)
            => uniquesCsv
                .Split('\n')
                .Skip(1)
                .Select(line =>
                {
                    var itemData = line.Split(';');

                    if (itemData.Length < 4)
                        return null;

                    var properties = new List<ItemProperty>();
                    var name = itemData[0];
                    var type = itemData[3]
                        .ToTitleCase()
                        .Replace("Ancientarmor", "Ancient Armor")
                        .Replace("Battle Guantlets", "Battle Gauntlets")
                        .Replace("Cedarbow", "Cedar Bow")
                        .Replace("Hunter’S Bow", "Hunter’s Bow")
                        .Replace("Jo Stalf", "Jo Staff")
                        .Replace("2-Handed Sword", "Two-Handed Sword")
                        .Replace("Tresllised Armor", "Trellised Armor");
                    var itemCategory = itemCategories.FirstOrDefault(x => x.Name == type);
                    var minDamageNorm = 0d;
                    var maxDamageNorm = 0d;
                    var minDamagePercentPerLevel = 0d;
                    var maxDamagePercentPerLevel = 0d;
                    var minDamagePerLevel = 0d;
                    var maxDamagePerLevel = 0d;
                    var minDefensePercentPerLevel = 0d;
                    var maxDefensePercentPerLevel = 0d;
                    var minDefensePerLevel = 0d;
                    var maxDefensePerLevel = 0d;
                    CurrentItemName = name;

                    if (itemCategory == null)
                    {
						MissingItemTypes.Add((name, type));
                        return null;
                    }

                    for (var index = 4; index < itemData.Length; index += 4)
                    {
                        if (string.IsNullOrEmpty(itemData[index]))
                            continue;

						var propertyName = itemData[index];
						var property = propertyRecords.FirstOrDefault(_ => _.Name == propertyName);

						if (property == null)
						{
							MissingProperties.Add((name, propertyName));
							continue;
						}
						var propertyFormattedName = property.FormattedName;
						var propertyPar = (double)itemData[index + 1].ParseDoubleOrDefault();
						var propertyMinimum = itemData[index + 2].ParseDoubleOrDefault();
						var propertyMaximum = itemData[index + 3].ParseDoubleOrDefault();

          

						if (propertyFormattedName == "Extra Durability" ||
							propertyFormattedName == "Charged")
							continue;
                        if (propertyFormattedName == "Class Skill")
                        {
                            var skill = GetSkill(skillRecords, propertyPar, itemData[index + 1]);

                            if (skill != null)
                            {
                                propertyFormattedName = $"{skill.Name} {skill.Class}";
                                propertyPar = 0;
                            }
                        }
                        else if (propertyFormattedName == "Randclassskill1")
                        {
                            propertyMinimum = propertyMaximum = propertyMaximum / 6;
                            propertyFormattedName = "To All Skills (Class Specific)";
                        }
                        else if (propertyFormattedName == "Randclassskill" || propertyFormattedName == "Randclassskill1")
                        {
                            propertyMinimum = propertyMaximum = propertyMaximum / 2;
                            propertyFormattedName = "To All Skills (Class Specific)";
                        }
                        else if (propertyFormattedName == "Charges")
                        {
                            var skill = GetSkill(skillRecords, propertyPar, itemData[index + 1]);

                            if (skill != null)
                            {
                                propertyFormattedName = $"Level {propertyMaximum} {skill.Name} ({propertyMinimum}/{propertyMinimum} Charges)";
                                propertyPar = propertyMaximum = propertyMinimum = 0;
                            }
                        }
                        else if (new[] { "Cold", "Fire", "Poison", "Lightning", "Magic" }.Select(_ => _ + " Resist").Contains(propertyFormattedName))
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            var valueDisplayed = propertyMinimum > 0 ? $"+{value}" : $"-{value}";

                            propertyFormattedName = $"{propertyFormattedName} {valueDisplayed}%";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Cold Duration")
                        {
                            propertyMaximum /= 25;
                            propertyMinimum /= 25;
                            propertyFormattedName = $"Cold Duration : {propertyMinimum}-{propertyMaximum} Seconds";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Cannot Be Frozen" ||
                                 propertyFormattedName == "Knockback" ||
                                 propertyFormattedName == "Slain Monsters Rest In Peace" ||
                                 propertyFormattedName == "Indestructible" ||
                                 propertyFormattedName == "Prevent Monster Heal" ||
                                 propertyFormattedName == "Ignore Target's Defense" ||
                                 propertyFormattedName == "Half Freeze Duration" ||
                                 propertyFormattedName == "Replenishes Quantity" ||
                                 propertyFormattedName == "Increased Stack Size")
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        else if (propertyFormattedName == "Ethereal")
                        {
                            propertyFormattedName = "Ethereal (cannot be repaired)";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Other Skill")
                        {
                            var skill = GetSkill(skillRecords, propertyPar, itemData[index + 1]);

                            propertyFormattedName = skill.Name;
                            propertyPar = 0;
                        }
                        else if (propertyFormattedName == "Reduce Magic Damage")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"Magic Damage Reduced By {value}";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Reduce Damage" ||
                                 propertyFormattedName == "Damage Reduced")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"Damage Reduced By {value}";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Defense (based on character level)")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"{value} {propertyFormattedName}";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Repair-Durability")
                        {
                            propertyFormattedName = $"Repairs 1 Durability in {propertyPar} seconds";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "To All Resistances")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"All Resistances +{value}";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Regenerate Mana")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"Regenerate Mana {value}%";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Howl")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"Hit Causes Monster To Flee {value}%";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Sockets")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"Socketed ({value})";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Fire Damage" ||
                                 propertyFormattedName == "Cold Damage" ||
                                 propertyFormattedName == "Poison Damage" ||
                                 propertyFormattedName == "Lightning Damage" ||
                                 propertyFormattedName == "Magic Damage" ||
                                 propertyFormattedName == "Damage")
                        {
                            if (propertyName == "dmg-norm")
                            {
                                minDamageNorm = propertyMinimum;
                                maxDamageNorm = propertyMaximum;
                            }
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"Adds {value} {propertyFormattedName}";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Pierce Attack")
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        else if (propertyFormattedName == "Replenish Life")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"Replenish Life +{value}";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Reduce Req %")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"Requirements {value}%";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Damage-Armor Class")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"{value} To Monster Defense Per Hit";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Freeze Duration")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"Freezes Target +{value}";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Reduce Poison Length")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"Poison Length Reduced By {value}%";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Hitpoint % Increase")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"Increase Maximum Life {value}%";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Dmg-Pois")
                        {
                            propertyPar /= 25;
                            if (propertyPar == 0)
                                propertyPar = 1;
                            propertyMinimum = propertyMinimum / propertyPar;
                            propertyMaximum = propertyMaximum / propertyPar;

                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"{value} Poison Damage Over {propertyPar} Seconds";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Slow Target" ||
                                 propertyFormattedName == "Slow Target %")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"Slows Target By {value}%";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Increase Maximum Mana")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"Increase Maximum Mana {value}%";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Reduce Dmg %")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"Damage Reduced By {value}%";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Attacker Takes Damage")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"Attacker Takes Damage Of {value}";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Regenerate Stamina")
                        {
                            var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                            propertyFormattedName = $"Heal Stamnia Plus {value}%";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else if (propertyFormattedName == "Class Skill Tab")
                        {
                            var skillTab = GetSkillTab(skillTabRecords, propertyPar, itemData[index + 1]);

                            if (skillTab != null)
                            {
                                propertyFormattedName = $"{skillTab.Name} {skillTab.Class}";
                                propertyPar = 0;
                            }
                        }
                        else if (propertyFormattedName == "Hit-Skill")
                        {
                            var skill = GetSkill(skillRecords, propertyPar, itemData[index + 1]);
                            if (skill != null)
                            {
                                var value = propertyMinimum == propertyMaximum ? Math.Round(propertyMinimum).ToString() : $"{Math.Round(propertyMinimum)}-{Math.Round(propertyMaximum)}";
                                propertyFormattedName = $"Chance To Cast Level {Math.Max(propertyMinimum, propertyMaximum)} {skill.Name} On Striking";
                                propertyMaximum = propertyMinimum;
                                propertyPar = 0;
                                property.IsPercent = true;
                            }
                        }
                        else if (propertyFormattedName == "Gethit-Skill")
                        {
                            var skill = GetSkill(skillRecords, propertyPar, itemData[index + 1]);

                            if (skill != null)
                            {
                                propertyFormattedName = $"Chance To Cast Level {propertyMaximum} {skill.Name} When Struck";
                                propertyMaximum = propertyMinimum;
                                propertyPar = 0;
                            }
                        }
                        else if (propertyFormattedName.Contains("(Based On Character Level)") && propertyPar != 0)
                        {
                            var percent = property.IsPercent ? "%" : string.Empty;
                            var min = Math.Round(propertyPar / 8);
                            var max = Math.Round(propertyPar * 99 / 8);

                            if (propertyName == "dmg%/lvl")
                            {
                                minDamagePercentPerLevel = min;
                                maxDamagePercentPerLevel = max;
                            }
                            if (propertyName == "dmg/lvl")
                            {
                                minDamagePerLevel = min;
                                maxDamagePerLevel = max;
                            }
                            if (propertyName == "ac%/lvl")
                            {
                                minDefensePercentPerLevel = min;
                                maxDefensePercentPerLevel = max;
                            }
                            if (propertyName == "ac/lvl")
                            {
                                minDefensePerLevel = min;
                                maxDefensePerLevel = max;
                            }

                            propertyFormattedName = $"{min}-{max}{percent} {propertyFormattedName}";
                            propertyPar = propertyMaximum = propertyMinimum = 0;
                        }
                        else
                            propertyPar /= 8;

						properties.Add(new ItemProperty
                        {
							Name = propertyName,
							FormattedName = propertyFormattedName,
                            Par = propertyPar,
                            Minimum = propertyMinimum,
                            Maximum = propertyMaximum,
                            IsPercent = property.IsPercent,
							Id = Guid.NewGuid(),
							FirstChararacter = property.FirstChararacter,
							OrderIndex = property.OrderIndex
						});
                    }

					var allStats = properties.Where(_ => new[] { "Strength", "Dexterity", "Vitality", "Energy" }.Contains(_.Name));

					if (allStats.Count() == 4 &&
						allStats.Select(_ => _.Minimum).Distinct().Count() == 1 &&
						allStats.Select(_ => _.Maximum).Distinct().Count() == 1)
					{
						var firstAttribute = allStats.First();

						properties.Add(new ItemProperty()
						{
							Name = "To All Attributes",
							FormattedName = "To All Attributes",
							Par = 0,
							Minimum = firstAttribute.Minimum,
							Maximum = firstAttribute.Maximum,
							IsPercent = firstAttribute.IsPercent,
							Id = Guid.NewGuid(),
							FirstChararacter = firstAttribute.FirstChararacter,
						});

						properties.RemoveAll(_ => new[] { "Strength", "Dexterity", "Vitality", "Energy" }.Contains(_.Name));
					}

					new[] { "fire", "ltng", "pois", "cold", "elem" }
					 .ToList()
					 .ForEach(elem =>
					 {
						 var elemDamage = properties.Where(_ => new[] { elem + "-min", elem + "-max" }.Contains(_.Name));
						 var elementName =
							elem == "fire" ? "Fire" :
							elem == "ltng" ? "Lightning" :
							elem == "pois" ? "Poison" :
							elem == "cold" ? "Cold" :
							"Elemental";

						 // Merge ?

						 if (elemDamage.Count() == 2)
						 {
							 var minimum = elemDamage.First(_ => _.Name == elem + "-min").Minimum;
							 var maximum = elemDamage.First(_ => _.Name == elem + "-max").Maximum;
							 var value = minimum == maximum ? minimum.ToString() : $"{minimum}-{maximum}";
							 var propertyFormattedName = $"Adds {value} {elementName} Damage";

							 var propertyDamage = properties.FirstOrDefault(_ => _.Name == $"dmg-{elem}");

							 if (propertyDamage == null)
							 {
								 properties.Add(new ItemProperty()
								 {
									 Name = $"dmg-{elem}",
									 FormattedName = propertyFormattedName,
									 Par = 0,
									 Minimum = 0,
									 Maximum = 0,
									 IsPercent = false,
									 Id = Guid.NewGuid(),
									 FirstChararacter = string.Empty
								 });
							 }
							 else
							 {
								 propertyDamage.Minimum += minimum;
								 propertyDamage.Maximum += maximum;
							 }

							 properties.RemoveAll(_ => new[] { elem + "-min", elem + "-max" }.Contains(_.Name));
	 					}
					 });
					

					var minimumDamage = GetPropertyValueOrDefault(properties, "dmg-min") + GetPropertyValueOrDefault(properties, "dmg-norm");
					var maximumDamage = GetPropertyValueOrDefault(properties, "dmg-max") + GetPropertyValueOrDefault(properties, "dmg-norm", ItemPropertyType.Maximum);
					var damagePercentMinimum = GetPropertyValueOrDefault(properties, "Damage %");
					var damagePercentMaximum = GetPropertyValueOrDefault(properties, "Damage %", ItemPropertyType.Maximum);

                    var defenseMinimum = GetPropertyValueOrDefault(properties, "Armor Class");
					var defenseMaximum = GetPropertyValueOrDefault(properties, "Armor Class", ItemPropertyType.Maximum);
					var defensePercentMinimum = GetPropertyValueOrDefault(properties, "Armor Class %");
					var defensePercentMaximum = GetPropertyValueOrDefault(properties, "Armor Class %", ItemPropertyType.Maximum);

					var requirementPercent = GetPropertyValueOrDefault(properties, "Reduce Req %");

					return new Item
					{
						Id = Guid.NewGuid(),
						Name = name,
						LevelRequired = itemData[2].ParseDoubleOrDefault(),
						Level = itemData[1].ParseDoubleOrDefault(),
						Quality = "Unique",
						Properties = properties.OrderBy(_ => _.OrderIndex).ToList(),
                        Category = itemCategory?.Category,
                        SubCategory = itemCategory?.SubCategory,
						Type = type,
						// Specific to Armor :
						MinimumDefenseMinimum = (int)(itemCategory?.MinimumDefense * (defensePercentMinimum + minDefensePercentPerLevel + 100)) / 100 + defenseMinimum + minDefensePerLevel,
						MaximumDefenseMinimum = (int)(itemCategory?.MaximumDefense * (defensePercentMinimum + minDefensePercentPerLevel + 100)) / 100 + defenseMinimum + minDefensePerLevel,
						MinimumDefenseMaximum = (int)(itemCategory?.MinimumDefense * (defensePercentMaximum + maxDefensePercentPerLevel + 100)) / 100 + defenseMaximum + maxDefensePerLevel,
						MaximumDefenseMaximum = (int)(itemCategory?.MaximumDefense * (defensePercentMaximum + maxDefensePercentPerLevel + 100)) / 100 + defenseMaximum + maxDefensePerLevel,
						// Specific to Weapon :
						MinimumOneHandedDamageMinimum = Math.Round(((itemCategory?.MinimumOneHandedDamage * (damagePercentMinimum + minDamagePercentPerLevel + 100)) / 100).AddIfPositive(minimumDamage + minDamagePerLevel + minDamageNorm)),
						MaximumOneHandedDamageMinimum = Math.Round(((itemCategory?.MaximumOneHandedDamage * (damagePercentMinimum + minDamagePercentPerLevel + 100)) / 100).AddIfPositive(minimumDamage + minDamagePerLevel + minDamageNorm)),
						MinimumTwoHandedDamageMinimum = Math.Round(((itemCategory?.MinimumTwoHandedDamage * (damagePercentMinimum + minDamagePercentPerLevel + 100)) / 100).AddIfPositive(minimumDamage + minDamagePerLevel + minDamageNorm)),
						MaximumTwoHandedDamageMinimum = Math.Round(((itemCategory?.MaximumTwoHandedDamage * (damagePercentMinimum + minDamagePercentPerLevel + 100)) / 100).AddIfPositive(minimumDamage + minDamagePerLevel + minDamageNorm)),
						MinimumOneHandedDamageMaximum = Math.Round(((itemCategory?.MinimumOneHandedDamage * (damagePercentMaximum + maxDamagePercentPerLevel + 100)) / 100).AddIfPositive(maximumDamage + maxDamagePerLevel + maxDamageNorm)),
						MaximumOneHandedDamageMaximum = Math.Round(((itemCategory?.MaximumOneHandedDamage * (damagePercentMaximum + maxDamagePercentPerLevel + 100)) / 100).AddIfPositive(maximumDamage + maxDamagePerLevel + maxDamageNorm)),
						MinimumTwoHandedDamageMaximum = Math.Round(((itemCategory?.MinimumTwoHandedDamage * (damagePercentMaximum + maxDamagePercentPerLevel + 100)) / 100).AddIfPositive(maximumDamage + maxDamagePerLevel + maxDamageNorm)),
						MaximumTwoHandedDamageMaximum = Math.Round(((itemCategory?.MaximumTwoHandedDamage * (damagePercentMaximum + maxDamagePercentPerLevel + 100)) / 100).AddIfPositive(maximumDamage + maxDamagePerLevel + maxDamageNorm)),
						// Stats
						StrengthRequired = (itemCategory?.StrengthRequired * (requirementPercent + 100)) / 100,
						DexterityRequired = (itemCategory?.DexterityRequired * (requirementPercent + 100)) / 100,
					};
                })
                .Where(item => item != null)
                .ToList();

		private List<SkillRecord> ReadSkills(string skillsCsv)
			=> skillsCsv
				.Split('\n')
					.Select(line =>
					{
						var itemData = line.Split(';');

						if (itemData.Length < 3)
							return null;

						return new SkillRecord
						{
							Name = itemData[0].ToTitleCase(),
							Id = itemData[1].ParseIntOrDefault(),
							Class = itemData[2]
								.Replace("\r", string.Empty)
								.ReplaceIfEquals("ama", "(Amazon Only)")
								.ReplaceIfEquals("ass", "(Assassin Only)")
								.ReplaceIfEquals("bar", "(Barbarian Only)")
								.ReplaceIfEquals("dru", "(Druid Only)")
								.ReplaceIfEquals("pal", "(Paladin Only)")
								.ReplaceIfEquals("nec", "(Necromancer Only)")
								.ReplaceIfEquals("sor", "(Sorceress Only)")
						};
					})
					.Where(item => item != null)
				.ToList();

		private List<SkillTabRecord> ReadSkillTabs(string skillTabsCsv)
			=> skillTabsCsv
				.Split('\n')
					.Select(line =>
					{
						var itemData = line.Split(';');

						if (itemData.Length < 3)
							return null;

						return new SkillTabRecord
						{
							Id = itemData[0].ParseIntOrDefault(),
							Name = itemData[1].ToTitleCase(),
							Class = $"({itemData[2]} Only)"
								.Replace("\r", string.Empty)
								.ToTitleCase()
						};
					})
					.Where(item => item != null)
				.ToList();

		private List<PropertyRecord> ReadProperties(string propertiesCsv)
			=> propertiesCsv
				.Split('\n')
				.Select(line =>
				{
					var itemData = line.Split(';');

					if (itemData.Length < 3)
						return null;

					return new PropertyRecord
					{
						Name = itemData[0],
						FormattedName = itemData[1].ToTitleCase(),
						IsPercent = itemData[2].ParseIntOrDefault() == 1,
						FirstChararacter = itemData[3],
						OrderIndex = itemData[4].ParseDoubleOrDefault(),
					};
				})
				.Where(item => item != null)
				.ToList();

		private List<ArmorReord> ReadArmors(string armorsCsv)
            => armorsCsv
                .Split('\n')
                .Skip(1)
                .Select(line =>
                {
                    var itemData = line.Split(';');

                    if (itemData.Length < 2)
                        return null;

                    return new ArmorReord
                    {
                        Name = itemData[0],
                        Slot = itemData[1]
                            .Replace("\t", string.Empty)
                            .Replace("\\", string.Empty),
						MinimumDefense = itemData[2].ParseDoubleOrDefault(),
						MaximumDefense = itemData[3].ParseDoubleOrDefault(),
						StrengthRequired = itemData[4].ParseDoubleOrDefault()
                    };
                })
                .Where(item => item != null)
                .ToList();

        private List<WeaponRecord> ReadWeapons(string weaponsCsv) 
            => weaponsCsv
                .Split('\n')
                .Skip(1)
                .Select(line =>
                {
                    var itemData = line.Split(';');

                    if (itemData.Length < 3)
                        return null;

					return new WeaponRecord
					{
						Name = itemData[0],
						Type = itemData[1],
						Slot = itemData[2].Replace("\r", string.Empty),
						MinimumOneHandedDamage = itemData[3].ParseDoubleOrDefault(),
						MaximumOneHandedDamage = itemData[4].ParseDoubleOrDefault(),
						MinimumTwoHandedDamage = itemData[5].ParseDoubleOrDefault(),
						MaximumTwoHandedDamage = itemData[6].ParseDoubleOrDefault(),
						StrengthRequired = itemData[7].ParseDoubleOrDefault(),
						DexterityRequired = itemData[8].ParseDoubleOrDefault(),
					};
				})
                .Where(item => item != null)
                .ToList();

        private List<ItemCategoryRecord> ReadSubCategories(
            List<WeaponRecord> weapons,
            List<ArmorReord> armors)
        {
            var armorSubCategoriesRecord = armors
                .Select(armor => new ItemCategoryRecord
                {
                    Name = armor.Name
                        .Replace("\r", string.Empty)
                            .Replace("(M)", " Medium")
                            .Replace("(H)", " Heavy")
                            .Replace("(L)", " Light")
                            .Replace("Hard Leather Armor", "Hard Leather")
                            .Replace("Gaunlets", "Gauntlets")
                            .Replace("Cap/hat", "Cap")
                            .Replace("Skull  Guard", "Skull Guard"),
                    SubCategory = armor.Slot
                    .Replace("_", " ")
                    .Replace("\t", string.Empty)
                    .Replace("\"", string.Empty)
                    .Replace("\\", string.Empty)
                    .ToTitleCase(),
                    Category = "Armor",
					MinimumDefense = armor.MinimumDefense,
					MaximumDefense = armor.MaximumDefense,
					StrengthRequired = armor.StrengthRequired,
                })
                .Where(record => record.SubCategory != string.Empty)
                .ToList();

			var weaponSubCategoriesRecord = weapons
				.Where(weapon => !string.IsNullOrWhiteSpace(weapon.Type))
				.Select(weapon => new ItemCategoryRecord
				{
					Name = weapon.Name
						.Replace("Bec-de-Corbin", "Bec-De-Corbin")
						.Replace("Kriss", "Kris")
						.Replace("Martel de Fer", "Martel De Fer")
						.Replace("Blood Spirt", "Blood Spirit")
						.Replace("Hunter's Bow", "Hunter’s Bow")
						.Replace("MatriarchalJavelin", "Matriarchal Javelin"),
					Category = "Weapon",
					SubCategory =
						(weapon.Slot == "1h" ? weapon.Type :
						 weapon.Slot == "2h" ? $"Two Handed {weapon.Type}" :
						 $"Two And One Handed {weapon.Type}")
							.ToTitleCase()
							.Replace("Scep", "Scepter")
							.Replace("Hamm", "Hammer")
							.Replace("Swor", "Sword")
							.Replace("Knif", "Knife")
							.Replace("Jave", "Javelin")
							.Replace("Jave", "Jave")
							.Replace("Spea", "Spear")
							.Replace("Pole", "Polearm")
							.Replace("Staf", "Staff")
							.Replace("Xbow", "Crossbow")
							.Replace("Tpot", "Throwing Potions")
							.Replace("Taxe", "Throwing Axe")
							.Replace("Tkni", "Thorwing knife")
							.Replace("Abow", "Amazon bow")
							.Replace("Aspe", "Amazon spear")
							.Replace("Ajav", "Amazon Javelin")
							.Replace("H2h2", "Hand To Hand Two Handed")
							.Replace("H2h", "Hand To Hand")
							.Replace("Javelinlin", "Javelin")
							.Replace("Spearr", "Spear")
							.Replace("\t", string.Empty)
							.Replace("\\", string.Empty)
							.ToTitleCase(),
					MinimumOneHandedDamage = weapon.MinimumOneHandedDamage,
					MaximumOneHandedDamage = weapon.MaximumOneHandedDamage,
					MinimumTwoHandedDamage = weapon.MinimumTwoHandedDamage,
					MaximumTwoHandedDamage = weapon.MaximumTwoHandedDamage,
					StrengthRequired = weapon.StrengthRequired,
					DexterityRequired = weapon.DexterityRequired
				})
                .ToList();

            weaponSubCategoriesRecord.AddRange(armorSubCategoriesRecord);
            weaponSubCategoriesRecord.AddRange(new[]
            {
					new ItemCategoryRecord { Name = "Silver-Edged Axe", Category = "Weapon", SubCategory = "Two Handed Axe", MinimumTwoHandedDamage = 23, MaximumTwoHandedDamage= 41, StrengthRequired = 154 },
                    new ItemCategoryRecord { Name = "Amulet", Category = "Jewelry", SubCategory = "Amulet"},
                    new ItemCategoryRecord { Name = "Arrows", Category = "Armor", SubCategory = "Arrows"},
                    new ItemCategoryRecord { Name = "Bolts", Category = "Armor", SubCategory = "Bolts"},
                    new ItemCategoryRecord { Name = "Charm", Category = "Charm", SubCategory = "Charm"},
                    new ItemCategoryRecord { Name = "Jewel", Category = "Jewelry", SubCategory = "Jewel"},
                    new ItemCategoryRecord { Name = "Ring", Category = "Jewelry", SubCategory = "Ring"},
                    new ItemCategoryRecord { Name = "Conqueror Crown", Category = "Armor", SubCategory = "Barbarian Helm", MinimumDefense = 114, MaximumDefense = 159, StrengthRequired = 174},
                    new ItemCategoryRecord { Name = "Blood Spirit", Category = "Armor", SubCategory = "Druid Helm", MinimumDefense = 101, MaximumDefense = 145, StrengthRequired = 80},
					new ItemCategoryRecord { Name = "Sash", Category = "Armor", SubCategory = "Armor", MinimumDefense = 218, MaximumDefense = 233, StrengthRequired = 88},
                    new ItemCategoryRecord { Name = "Girdle", Category = "Armor", SubCategory = "Waist", MinimumDefense = 12, MaximumDefense = 15, StrengthRequired = 53},
            });

            return weaponSubCategoriesRecord;
        }

		public double GetPropertyValueOrDefault(List<ItemProperty> properties, string name, ItemPropertyType type = ItemPropertyType.Minimum)
		{
			var property = properties.FirstOrDefault(_ => _.Name == name);

            return property == null ? 0 :
                    type == ItemPropertyType.Minimum ? property.Minimum :
                    type == ItemPropertyType.Maximum ? property.Maximum :
                                                       property.Par * 99;
		}

		public SkillRecord GetSkill(List<SkillRecord> skills, double id, string name = null)
		{
			var skill = skills.FirstOrDefault(_ => _.Id == Convert.ToInt32(id));

			if (skill == null)
				skill = skills.FirstOrDefault(_ => _.Name == name.ToTitleCase());
			
			if (skill == null)
				MissingSkills.Add((name, CurrentItemName, id));

			return skill;
		}

		public SkillTabRecord GetSkillTab(List<SkillTabRecord> skillTabs, double id, string name = null)
		{
			var skillTab = skillTabs.FirstOrDefault(_ => _.Id == Convert.ToInt32(id));

			if (skillTab == null)
				skillTab = skillTabs.FirstOrDefault(_ => _.Name == name.ToTitleCase());

			if (skillTab == null)
				MissingSkills.Add((name, CurrentItemName, id));

			return skillTab;
		}
	}

	public static class DoubleExtensions
	{
		public static double AddIfPositive(this double? value, double toAdd) => value.Value > 0 ? value.Value + toAdd : value.Value;
		public static double AddIfPositive(this double value, double toAdd) => value > 0 ? value + toAdd : value;
	}
}