using DiabloII.Items.Reader.Extensions;
using DiabloII.Items.Reader.Records;
using System;
using System.Collections.Generic;
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
			var missingProps = string.Join(Environment.NewLine, uniques.SelectMany(_ => _.Properties.Select(p => p.Name).Where(name => properties.FirstOrDefault(s => s.Name == name) == null).Distinct()).Distinct().ToList());
			var missingItemsCategories = string.Join(Environment.NewLine, MissingItemTypes.OrderBy(_ => _.Name).Distinct().Select(_ => $"{_.Name} : {_.Type}"));
			var missingProperties = string.Join(Environment.NewLine, MissingProperties.OrderBy(_ => _.Name).Distinct().Select(_ => $"{_.Name} : {_.Property}"));
			var missingSkills = string.Join(Environment.NewLine, MissingSkills.OrderBy(_ => _.Name).Distinct().Select(_ => $"{_.ItemName} - {_.Name} : {_.Id}"));

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
						var propertyPar = (double)itemData[index + 1].ParseIntOrDefault();
						var propertyMinimum = itemData[index + 2].ParseIntOrDefault();
						var propertyMaximum = itemData[index + 3].ParseIntOrDefault();

						if (propertyFormattedName == "Class Skill")
						{
							var skill = GetSkill(skillRecords, Convert.ToInt32(propertyPar), itemData[index + 1]);

							propertyFormattedName = $"{skill.Name} {skill.Class}";
							propertyPar = 0;
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
							var skill = GetSkill(skillRecords, Convert.ToInt32(propertyPar), itemData[index + 1]);

							if (skill != null)
							{
								propertyFormattedName = $"Level {propertyMaximum} {skill.Name} ({propertyMinimum}/{propertyMinimum} Charges)";
								propertyPar = propertyMaximum = propertyMinimum = 0;
							}
						}
						else if (propertyFormattedName == "Ignore Armor")
						{
							propertyFormattedName = "Ignore target's defense";
							propertyMaximum = propertyMinimum = 0;
						}
						else if (propertyFormattedName == "Other Skill")
						{
							var skill = GetSkill(skillRecords, Convert.ToInt32(propertyPar), itemData[index + 1]);

							propertyFormattedName = skill.Name;
							propertyPar = 0;
						}
						else if (propertyFormattedName == "Class Skill Tab")
						{
							var skillTab = GetSkillTab(skillTabRecords, Convert.ToInt32(propertyPar), itemData[index + 1]);

							if (skillTab != null)
							{
								propertyFormattedName = $"{skillTab.Name} {skillTab.Class}";
								propertyPar = 0;
							}
						}
						else if (propertyFormattedName == "Hit-Skill")
						{
							var skill = GetSkill(skillRecords, Convert.ToInt32(propertyPar), itemData[index + 1]);

							if (skill == null)
							{
								propertyFormattedName = $"Level {itemData[index + 7].ParseIntOrDefault()} {itemData[index + 5]} ({itemData[index + 6].ParseIntOrDefault()} {itemData[index + 4]})";
								propertyPar = propertyMinimum = propertyMaximum = 0;
								index += 3;

							}
							else
							{
								propertyFormattedName = $"Chance To Cast Level {propertyMaximum} {skill.Name} On Striking";
								propertyMaximum = propertyMinimum;
								propertyPar = 0;
								property.IsPercent = true;
							}
						}
						else if (propertyFormattedName == "Gethit-Skill")
						{
							var skill = GetSkill(skillRecords, Convert.ToInt32(propertyPar), itemData[index + 1]);

							if (skill != null)
							{
								propertyFormattedName = $"Chance To Cast Level {propertyMaximum} {skill.Name} When Struck";
								propertyMaximum = propertyMinimum;
								propertyPar = 0;
							}
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
							Id = Guid.NewGuid()
						});
                    }

					var minimumDamage = GetPropertyValueOrDefault(properties, "dmg-min") + GetPropertyValueOrDefault(properties, "dmg-norm");
					var maximumDamage = GetPropertyValueOrDefault(properties, "dmg-max") + GetPropertyValueOrDefault(properties, "dmg-norm", ItemPropertyType.Maximum);
					var damagePerLevel = GetPropertyValueOrDefault(properties, "dmg/lvl", ItemPropertyType.Par);
					var damagePercentMinimum = GetPropertyValueOrDefault(properties, "Damage %") + 100;
					var damagePercentMaximum = GetPropertyValueOrDefault(properties, "Damage %", ItemPropertyType.Maximum) + 100;
					var damagePercentPerLevel = GetPropertyValueOrDefault(properties, "dmg%/lvl", ItemPropertyType.Par);

					var defenseMinimum = GetPropertyValueOrDefault(properties, "Armor Class");
					var defenseMaximum = GetPropertyValueOrDefault(properties, "Armor Class", ItemPropertyType.Maximum);
					var defensePerLevel = GetPropertyValueOrDefault(properties, "ac/lvl", ItemPropertyType.Par);
					var defensePercentMinimum = GetPropertyValueOrDefault(properties, "Armor Class %") + 100;
					var defensePercentMaximum = GetPropertyValueOrDefault(properties, "Armor Class %", ItemPropertyType.Maximum) + 100;
					var defensePercentPerLevel = GetPropertyValueOrDefault(properties, "ac%/lvl", ItemPropertyType.Par);

					var requirementPercent = 100 + GetPropertyValueOrDefault(properties, "Reduce Req %");

					return new Item
                    {
						Id = Guid.NewGuid(),
						Name = name,
                        LevelRequired = itemData[2].ParseIntOrDefault(),
                        Level = itemData[1].ParseIntOrDefault(),
                        Quality = "Unique",
                        Properties = properties,
                        Category = itemCategory?.Category,
                        SubCategory = itemCategory?.SubCategory,
						Type = type,
						// Specific to Armor :
						MinimumDefenseMinimum = (itemCategory?.MinimumDefense * defensePercentMinimum) / 100 + defenseMinimum + defensePerLevel / 99,
						MaximumDefenseMinimum = (itemCategory?.MaximumDefense * defensePercentMinimum) / 100 + defenseMinimum + defensePerLevel / 99,
						MinimumDefenseMaximum = (itemCategory?.MinimumDefense * defensePercentMaximum + defensePercentPerLevel) / 100 + defenseMaximum + defensePerLevel,
						MaximumDefenseMaximum = (itemCategory?.MaximumDefense * defensePercentMaximum + defensePercentPerLevel) / 100 + defenseMaximum + defensePerLevel,
						// Specific to Weapon :
						MinimumOneHandedDamageMinimum = ((itemCategory?.MinimumOneHandedDamage * (damagePercentMinimum)) / 100).AddIfPositive(minimumDamage),
						MaximumOneHandedDamageMinimum = ((itemCategory?.MaximumOneHandedDamage * (damagePercentMinimum)) / 100).AddIfPositive(minimumDamage),
						MinimumTwoHandedDamageMinimum = ((itemCategory?.MinimumTwoHandedDamage * (damagePercentMinimum)) / 100).AddIfPositive(minimumDamage),
						MaximumTwoHandedDamageMinimum = ((itemCategory?.MaximumTwoHandedDamage * (damagePercentMinimum)) / 100).AddIfPositive(minimumDamage + damagePerLevel / 99),
						MinimumOneHandedDamageMaximum = ((itemCategory?.MinimumOneHandedDamage * (damagePercentMaximum + damagePercentPerLevel)) / 100 ).AddIfPositive(maximumDamage + damagePerLevel),
						MaximumOneHandedDamageMaximum = ((itemCategory?.MaximumOneHandedDamage * (damagePercentMaximum + damagePercentPerLevel)) / 100).AddIfPositive(maximumDamage + damagePerLevel),
						MinimumTwoHandedDamageMaximum = ((itemCategory?.MinimumTwoHandedDamage * (damagePercentMaximum + damagePercentPerLevel)) / 100).AddIfPositive(maximumDamage + damagePerLevel),
						MaximumTwoHandedDamageMaximum = ((itemCategory?.MaximumTwoHandedDamage * (damagePercentMaximum + damagePercentPerLevel)) / 100).AddIfPositive(maximumDamage + damagePerLevel),
						// Stats
						StrengthRequired = (itemCategory?.StrengthRequired * requirementPercent) / 100,
						DexterityRequired = (itemCategory?.DexterityRequired * requirementPercent) / 100,
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
						IsPercent = itemData[2].ParseIntOrDefault() == 1
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
                        Slot = itemData[1],
						MinimumDefense = itemData[2].ParseIntOrDefault(),
						MaximumDefense = itemData[3].ParseIntOrDefault(),
						StrengthRequired = itemData[4].ParseIntOrDefault()
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
						MinimumOneHandedDamage = itemData[3].ParseIntOrDefault(),
						MaximumOneHandedDamage = itemData[4].ParseIntOrDefault(),
						MinimumTwoHandedDamage = itemData[5].ParseIntOrDefault(),
						MaximumTwoHandedDamage = itemData[6].ParseIntOrDefault(),
						StrengthRequired = itemData[7].ParseIntOrDefault(),
						DexterityRequired = itemData[8].ParseIntOrDefault(),
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
                    SubCategory = armor.Slot.Replace("_", " ").ToTitleCase(),
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

		public int GetPropertyValueOrDefault(List<ItemProperty> properties, string name, ItemPropertyType type = ItemPropertyType.Minimum)
		{
			var property = properties.FirstOrDefault(_ => _.Name == name);

			return property == null ? 0 :
					type == ItemPropertyType.Minimum ? property.Minimum :
					type == ItemPropertyType.Maximum ? property.Maximum :
													   Convert.ToInt32(property.Par * 99);
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

	public static class IntExtension
	{
		public static int AddIfPositive(this int? value, int toAdd) => value.Value > 0 ? value.Value + toAdd : value.Value;
	}
}