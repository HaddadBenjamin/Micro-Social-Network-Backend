using System.Collections.Generic;
using System.Linq;

namespace DiabloII.Items.Reader
{
    public class Item
    {
        private ItemSubCategory subCategory;
        private ItemType type;

        public string Name { get; set; }
        public int LevelRequired { get; set; }
        // The following 4 fields should be an enum (an int) to be fast queried when it will be in a database.
        public string Quality { get; set; }
        public ItemType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;

                UpdateSubCategory();
            }
        }
        public ItemSubCategory SubCategory
        {
            get { return subCategory; }
            set
            {
                subCategory = value;
                UpdateCategory();
            }
        }
        public ItemCategory Category { get; set; }
        public IEnumerable<ItemProperty> Properties { get; set; }

        private void UpdateCategory()
            => Category =
                    new[]
                    {
                        ItemSubCategory.Armor,
                        ItemSubCategory.Glove,
                        ItemSubCategory.Belt,
                        ItemSubCategory.Boot,
                        ItemSubCategory.Shield,
                        ItemSubCategory.Helm
                    }.Contains(SubCategory) ? ItemCategory.Armor :
                    new[]
                    {
                        ItemSubCategory.Bow,
                        ItemSubCategory.Crossbow,
                        ItemSubCategory.Staff,
                        ItemSubCategory.Wand,
                        ItemSubCategory.Sword,
                        ItemSubCategory.Dagger,
                        ItemSubCategory.Axe,
                        ItemSubCategory.AxeWeapon, // to verify
                        ItemSubCategory.Lance,
                        ItemSubCategory.Masse,
                        ItemSubCategory.Scepter,
                        ItemSubCategory.ThrowWeapon,
                    }.Contains(SubCategory) ? ItemCategory.Weapon :
                    new[]
                    {
                        ItemSubCategory.Ring,
                        ItemSubCategory.Amulet,
                        ItemSubCategory.Jewel,
                    }.Contains(SubCategory) ? ItemCategory.Jewelry :
                    SubCategory == ItemSubCategory.Charm ? ItemCategory.Charm :
                    ItemCategory.UNKNOWN;

        private void UpdateSubCategory()
            => SubCategory =
                    new[]
                    {
                        ItemType.Ancient_Sword,
                        ItemType.Archon_Edge_Blade,
                    }.Contains(Type) ? ItemSubCategory.Sword :
                    new[]
                    {
                        ItemType.Angel_String_Bow,
                    }.Contains(Type) ? ItemSubCategory.Bow :
                    new[]
                    {
                        ItemType.Arbalest,
                    }.Contains(Type) ? ItemSubCategory.Crossbow :
                    new[]
                    {
                        ItemType.Hand_Axe,
                        ItemType.Axe,
                        ItemType.Ancient_Axe
                    }.Contains(Type) ? ItemSubCategory.Axe :
                    new[]
                    {
                        ItemType.Amazonian_Matron_Pike,
                        ItemType.Amazonian_Matron_Spear,
                        ItemType.Amazonian_Piercers,
                    }.Contains(Type) ? ItemSubCategory.Lance :
                    new[]
                    {
                        ItemType.Double_Axe,
                        ItemType.War_Axe,
                        ItemType.Military_Pick
                    }.Contains(Type) ? ItemSubCategory.AxeWeapon :
                    new[]
                    {
                        ItemType.Acrhon_Orb,
                        ItemType.Arcanist_Stick,
                        ItemType.Arcanite_Stick,
                    }.Contains(Type) ? ItemSubCategory.Wand :
                    new[]
                    {
                        ItemType.Aegis,
                        ItemType.Aerin_Shield,
                        ItemType.Ancient_Shield,
                    }.Contains(Type) ? ItemSubCategory.Shield :
                    new[]
                    {
                        ItemType.Amazonian_Girdle,
                        ItemType.Arcanic_Sash,
                    }.Contains(Type) ? ItemSubCategory.Belt :
                    new[]
                    {
                        ItemType.Amazonian_Hauberk,
                        ItemType.Amazonian_Plate,
                        ItemType.AncientArmor,
                        ItemType.Arcanic_Shroud,
                    }.Contains(Type) ? ItemSubCategory.Armor :
                    new[]
                    {
                       ItemType.Arcanic_Mitts,
                    }.Contains(Type) ? ItemSubCategory.Glove :
                    new[]
                    {
                        ItemType.Arcanic_Boots,
                    }.Contains(Type) ? ItemSubCategory.Boot :
                    new[]
                    {
                        ItemType.Amazonian_Tiara,
                        ItemType.Arcanic_Circlet,
                    }.Contains(Type) ? ItemSubCategory.Helm :
                    new[]
                    {
                        ItemType.Amulet,
                    }.Contains(Type) ? ItemSubCategory.Amulet :
                    ItemSubCategory.UNKNOWN;
        }
    }

    public enum ItemType
    {
        UNKNOWN,
        Acrhon_Orb,
        Aegis,
        Aerin_Shield,
        //Amazonian_Braces,
        //Amazonian_Caster,
        Amazonian_Girdle,
        //Amazonian_Guard,
        Amazonian_Hauberk,
        Amazonian_Matron_Pike,
        Amazonian_Matron_Spear,
        Amazonian_Piercers,
        Amazonian_Plate,
        //Amazonian_Striker,
        Amazonian_Tiara,
        //Amazonian_Treads,
        Amulet,
        Ancient_Axe,
        Ancient_Shield,
        Ancient_Sword,
        AncientArmor,
        //Angel_Cutter,
        Angel_String_Bow,
        Arbalest,
        Arcanic_Boots,
        Arcanic_Circlet,
        Arcanic_Mitts,
        Arcanic_Sash,
        Arcanic_Shroud,
        Arcanist_Stick,
        Arcanite_Stick,

        Archon_Edge_Blade,
        Archon_Soul_Shard,
        Archon_staff,
        Armet,
        Arrows,
        Assault_Helmet,
        Ataghan,
        Axe,
        Balanced_Crescent,
        Balanced_Mithril_Piercer,
        Balanced_Tomahawk,
        Balista,
        Balrog_blade,
        Balrog_Skin,
        Balrog_Skull,
        Balrog_spear,
        Barbarians_Edge,
        Barbed_Club,
        Barbed_Shield,
        Bardiche,
        Basinet,
        Bastard_Sword,
        Battle_Axe,
        Battle_Belt,
        Battle_Boots,
        Battle_cestus,
        Battle_dart,
        Battle_Guantlets,
        Battle_Hammer,
        Battle_Scythe,
        Battle_Staff,
        Battle_Sword,
        Bearded_Axe,
        Bec_de_Corbin,
        Belt,
        Berserker_axe,
        Bill,
        Blade,
        Blade_barrier,
        Blessed_Scepter,
        Blood_spirit,
        Bloodlord_skull,
        Bolts,
        Bone_Helm,
        Bone_Knife,
        Bone_Shield,
        Bone_visage,
        Bone_Wand,
        Bonebreaker_Mallet,
        Boneweave_boots,
        Bracers,
        Brandistock,
        Brawler_Blade,
        Breast_Plate,
        Broad_Axe,
        Broad_Sword,
        Buckler,
        Burnt_Wand,
        Caduceus,
        Cap,
        Carnage_Bow,
        Casque,
        Cedar_Staff,
        CedarBow,
        Ceremonial_Bow,
        Ceremonial_Caster,
        Ceremonial_Javelin,
        Ceremonial_Pike,
        Cestus,
        Chain_Boots,
        Chain_Mail,
        Champion_Axe,
        Champion_Sword,
        Chaos_Armor,
        Charm,
        Chu_Ko_Nu,
        Cinquedeas,
        Claymore,
        Cleaver,
        Club,
        Colossus_Blade,
        Colossus_crossbow,
        Composite_Bow,
        Conqueror_crown,
        Corona,
        Council_Spike,
        Council_War_Spike,
        Crossbow,
        Crowbill,
        Crown,
        Crown_Shield,
        Crusader_Bow,
        Crusader_Gauntlets,
        Cryptic_axe,
        Cryptic_sword,
        Crystal_Shillelah,
        Crystal_Sword,
        Cudgel,
        Cuirass,
        Cursed_Belt,
        Cursed_Boots,
        Cursed_Demon_Skull,
        Cursed_Gloves,
        Cursed_Hide,
        Cursed_Mask,
        Cursed_Visage,
        Cutlass,
        Dacian_Falx,
        Dagger,
        Dark_Archon_Plate,
        Dark_Archon_Staff,
        Dark_String_Crossbow,
        Dark_Summoner_Wand,
        Death_Mask,
        Decapitator,
        Deep_Carver_Point,
        Defender,
        Demon_Bone_Knife,
        Demon_crossbow,
        Demon_Fork,
        Demon_Head,
        Demon_Hook_Pilum,
        Demon_Plate,
        Demon_Visor,
        Demon_Ward,
        Demonhead,
        Demonhide_Armor,
        Demonhide_Boots,
        Demonhide_Gloves,
        Demonhide_Sash,
        Demonskin_Belt,
        Demonskin_Boots,
        Demonskin_Fleece,
        Demonskin_Mitts,
        Destroyer_helm,
        Devil_Star,
        Diadem,
        Diamon_Tip_Spear,
        Diamond_Edged_Mace,
        Diamondweave_Mesh,
        Dimensional_Blade,
        Dimensional_shard,
        Direwolf_Helm,
        Dirk,
        Divine_Scepter,
        Double_Axe,
        Double_Bow,
        Double_String_Bow,
        Dragon_Shield,
        Dream_Spirit,
        Druid_Helm,
        Dusk_shroud,
        Eagle_Crown,
        Earth_spirit,
        Edge_Bow,
        Elder_staff,
        Eldritch_orb,
        Elegant_blade,
        Elf_Wood_Bow,
        Embossed_Plate,
        Endless_Reaper,
        Espadon,
        Ethereal_Spear,
        Ettin_axe,
        Executioner_Sword,
        Falchion,
        Fanged_Helm,
        Fanged_knife,
        Fascia,
        Feral_claws,
        Field_Plate,
        Flail,
        Flamberge,
        Flanged_Mace,
        Flay_Scissors,
        Flying_axe,
        Francisca,
        Frenzy_Axe,
        Full_Helm,
        Full_Plate_Mail,
        Fullmoon_Visor,
        Fury_visor,
        Fuscina,
        Gauntlets,
        Gemmed_Cap,
        Ghost_Armor,
        Ghost_glaive,
        Giant_Axe,
        Giant_Ogre_Axe,
        Giant_Ogre_Glaive,
        Giant_Ogre_Maul,
        Giant_Sword,
        Giant_Thresher,
        Giants_Spear,
        Girdle,
        Gladius,
        Glorious_axe,
        Gloves,
        Gnarled_Staff,
        Gothic_Axe,
        Gothic_Bow,
        Gothic_Plate,
        Gothic_Shield,
        Gothic_Staff,
        Gothic_Sword,
        Grand_Amazonian_Caster,
        Grand_Crown,
        Grand_Scepter,
        Grave_Wand,
        Great_Axe,
        Great_Helm,
        Great_maul,
        Great_Maul,
        Great_Sword,
        Grim_Helm,
        Grim_Scythe,
        Grim_Shield,
        Grim_Wand,
        Halberd,
        Hammer,
        Hand_Axe,
        Hard_Leather,
        Harrogath_Blade,
        Harrogath_Visor,
        Hatchet,
        Hawk_Helm,
        Heavy_Belt,
        Heavy_Boots,
        Heavy_Bracers,
        Heavy_Crossbow,
        Heavy_Gloves,
        Heirophant_Trophy,
        Helm,
        Holy_Water_Sprinkler,
        Horadrim_Staff,
        Hunter_s_Bow,
        Hydra_Bow,
        Hyperion_spear,
        Jagged_Star,
        Jewel,
        Jo_Stalf,
        Katar,
        Kite_Shield,
        Knout,
        Kraken_shell,
        Kris,
        Kurast_Edge,
        Lance,
        Large_Axe,
        Large_Shield,
        Leather_Armor,
        Leather_Boots,
        Legend_spike,
        Legendary_mallet,
        Legendary_Mallet,
        Lich_wand,
        Light_Belt,
        Light_Crossbow,
        Light_Gauntlets,
        Light_Plate,
        Light_Plate_Boots,
        Lightforged_Guard,
        Lightforged_Plate,
        Linked_Mail,
        Lochaber_Axe,
        Long_Battle_Bow,
        Long_Bow,
        Long_Siege_Bow,
        Long_Staff,
        Long_Sword,
        Long_War_Bow,
        Luna,
        Lut_Gholein_Mail,
        Lut_Gholein_Scimitar,
        Lut_Gholein_Voulge,
        Mace,
        Mage_Plate,
        Martel_de_Fer,
        Mask,
        Matriarchal_bow,
        Matriarchal_javelin,
        Matriarchal_spear,
        Maul,
        Mesh_Armor,
        Mesh_Belt,
        Mesh_Boots,
        Mighty_scepter,
        Military_Axe,
        Military_Pick,
        Mithril_Barb_Club,
        Mithril_Bound_Maul,
        Mithril_coil,
        Mithril_Edged_Axe,
        Mithril_Long_Sword,
        Mithril_Pellet_Bow,
        Mithril_Piercer,
        Mithril_Short_Sword,
        Mithril_Tip_Javelin,
        Monarch,
        Morning_Star,
        Myrmidon_greaves,
        Naga,
        Ogre_axe,
        Ogre_Blade,
        Ogre_Bow,
        Ogre_Casting_Axe,
        Ogre_gauntlets,
        Ogre_Gauntlets,
        Ogre_maul,
        Ordos_Buckle,
        Ordos_Crown,
        Ordos_Defender,
        Ordos_Gauntlets,
        Ordos_Greaves,
        Ordos_Plate,
        Ordos_Spire,
        Ornate_Armor,
        Partizan,
        Pavise,
        Petrified_Wand,
        Phase_blade,
        Phase_Blade,
        Phase_Plate,
        Pike,
        Plate_Boots,
        Plate_Cleaver,
        Plate_Mail,
        Plated_Armet,
        Plated_Conch,
        Poignard,
        Poleaxe,
        Preserved_Head,
        Quarterstaff,
        Quick_String_Bow,
        Quilted_Armor,
        Raging_Venerator_Axe,
        Razor_Bow,
        Reaping_Thresher,
        Reinforced_Blade,
        Repeating_Crossbow,
        Repetitive_Crossbow,
        Ring,
        Ring_Mail,
        Rogue_Bow,
        Rondache,
        Rondel,
        Round_Shield,
        Rune_Bow,
        Rune_Scepter,
        Rune_Staff,
        Rune_Sword,
        Runic_Talons,
        Russet_Armor,
        Saber,
        Sacred_armor,
        Sacred_rondache,
        Sallet,
        Sash,
        Scale_Mail,
        Scarabshell_boots,
        Scepter,
        Scimitar,
        Scissors_Katar,
        Scourge,
        Scutum,
        Scythe,
        Seclorum_Guard,
        Seraphim_Rasper,
        Serated_Dagger,
        SerpentSkin_Armor,
        Shadow_plate,
        Shako,
        Shamshir,
        Sharkskin_Belt,
        Sharkskin_Boots,
        Sharkskin_Gloves,
        Sharktooth_Armor,
        Sharpened_Decapitator,
        Short_Battle_Bow,
        Short_Bow,
        Short_Siege_Bow,
        Short_Staff,
        Short_Sword,
        Short_War_Bow,
        Siege_Crossbow,
        Silver_edged_axe,
        Skull_Cap,
        Skull_Guard,
        Sky_spirit,
        Slayer_Guard,
        Small_Shield,
        Spear,
        Spetum,
        Spiderweb_sash,
        Spiked_Club,
        Spiked_Shield,
        Spired_helm,
        Spired_Helm,
        Spirit_Mask,
        Spiriting_Shard,
        Splint_Mail,
        Staff,
        Stilleto,
        Studded_Leather,
        Succubae_skull,
        Swirling_Crystal,
        Tabar,
        Templar_Coat,
        Thresher,
        Thunder_maul,
        Thunder_Maul,
        Tiara,
        Tigulated_Mail,
        Tomahawk,
        Tomb_Wand,
        Tower_Shield,
        Tresllised_Armor,
        Trident,
        Tristram_Crossbow,
        Tristram_Mail,
        Troll_nest,
        Truncheon,
        Tulwar,
        Tusk_Sword,
        Twin_Axe,
        Two_Handed_Sword,
        Tyrant_club,
        Unearthed_wand,
        Vambraces,
        Vampirebone_gloves,
        Vampirefang_belt,
        Venerator_Axe,
        Vengful_Flail,
        Vicious_Ettin_Axe,
        Violent_Impaler,
        Viper_Guard,
        Viper_Talon,
        Viperfang_Barrier,
        Viperskin_Belt,
        Viperskin_Boots,
        Viperskin_Braces,
        Viperskin_Helm,
        Viperskin_Husk,
        Voulge,
        Wand,
        War_Axe,
        War_Belt,
        War_Boots,
        War_Club,
        War_fork,
        War_Fork,
        War_Gauntlets,
        War_Hammer,
        War_Hat,
        War_pike,
        War_Scepter,
        War_Scythe,
        War_Spear,
        War_spike,
        War_Staff,
        War_Sword,
        Ward,
        Ward_bow,
        Warden_Bow,
        Warfare_Cestus,
        Warfare_Crown,
        Warforged_Aegis,
        Warforged_Axe,
        Warforged_Blade,
        Warforged_Buckle,
        Warforged_Gauntlets,
        Warforged_Greaves,
        Warforged_Plate,
        Winged_axe,
        Winged_harpoon,
        Winged_Helm,
        Winged_knife,
        Wire_Fleece,
        Wolf_Head,
        Wolverine_Claw,
        Woodfist_Club,
        Wrist_sword,
        Wyrmscale,
        Yari,
        Yew_Wand,
        Zakarum_Blade,
        Zakarum_Caster,
        Zakarum_Glaive,
        Zakarum_Halberd,
        Zakarum_Mace,
        Zakarum_Piercer,
        Zakarum_Scepter,
        Zakarum_shield,
        Zakarum_Skull,
        Zakarum_Staff,
        Zweihander
    }

    public enum ItemQuality
    {
        Normal,
        Magical,
        Rare,
        Unique,
        Set,
        Crafted
    }

    public enum ItemCategory
    {
        UNKNOWN,
        Weapon,
        Armor,
        Jewelry,
        Charm,
    }



    public class ItemProperty
    {
        // Maybe name should be an enum to be faster to query
        public string Name { get; set; }
        public int Par { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public bool IsPercent { get; set; }
    }

    public enum ItemSubCategory
    {
        UNKNOWN,
        // Armor
        Armor,
        Glove,
        Belt,
        Boot,
        Shield,
        Helm,
        // Weapon
        Bow,
        Crossbow,
        Staff,
        Wand,
        Sword,
        Dagger,
        Axe,
        AxeWeapon, // to verify
        Lance,
        Masse,
        Scepter,
        ThrowWeapon,
        // Jewelry
        Ring,
        Amulet,
        Jewel,
        // Charm
        Charm
    }

    public class DiabloIIDatasheetReader
    {
        public IEnumerable<Item> Read(string datasheetCsv)
        {
            // TODO : 
            // - Mapper types to a SubCategory
            // - Read properties
            // - Map properties to a readable format
            // - Add scyte and news items category in subcategory
            // - Add those data in a database : 1) front ask items for a type. 2) back service is call that ask the db those items. 3) it will be faster than generate all items each time.
            // - Create an enum for types.
            // - Trier les attributs : stat requis / damage / armure en premier / le reste trier en mode alphabétique ?

            return datasheetCsv
                .Split('\n')
                .Skip(1)
                .Select(line =>
                {
                    var a = line.Split(';');
                    if (a.Length < 4)
                        return null;

                    var type = string.IsNullOrEmpty(line) ? string.Empty : line.Split(';')[3];
                    var properties = new List<ItemProperty>();

                    for (var i = 4; i < a.Length; i += 4)
                    {
                        if (string.IsNullOrEmpty(a[i]))
                            continue;

                        properties.Add(new ItemProperty
                        {
                            Name = a[i],
                            Par = a[i + 1].ParseIntOrDefault(),
                            Minimum = a[i + 2].ParseIntOrDefault(),
                            Maximum = a[i + 3].ParseIntOrDefault(),
                            IsPercent = a[i].Contains("%")
                        });
                    }

                    var x = new Item
                    {
                        Name = a[0],
                        LevelRequired = a[2].ParseIntOrDefault(),
                        Type = type,
                        Quality = "Unique",
                        Properties = properties
                    };

                    return x;
                })
                .ToList();
        }

        private static string GenerateItemTypes(string datasheetCsv)
            => string.Join(",\n",
                datasheetCsv
                .Split('\n')
                .Skip(1)
                .Select(line => ItemTypeFormat(line))
                .Distinct()
                .OrderBy(_ => _)
                .Skip(1));

        private static string ItemTypeFormat(string line)
            => string.IsNullOrEmpty(line) ? string.Empty :
                    line.Split(';')[3]
                        .Replace("2", "two")
                        .Replace(' ', '_')
                        .Replace('’', '_')
                        .Replace('-', '_')
                        .FirstCharToUpper();
    }

    public static class StringExtension
    {
        public static int ParseIntOrDefault(this string text) => int.TryParse(text, out _) ? int.Parse(text) : 0;

        public static string FirstCharToUpper(this string text) => string.IsNullOrEmpty(text) ? text : char.ToUpper(text[0]) + text.Substring(1);
    }
}
