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
        public string Quality { get; set; }
        public string TypeValue { get { return Type.ToString().Replace('_', ' '); } }
        public string SubCategoryValue { get { return SubCategory.ToString(); } }
        public string CategoryValue { get { return Category.ToString(); } }
        public IEnumerable<ItemProperty> Properties { get; set; }

        // The following 3 fields should be an enum (an int) to be fast queried when it will be in a database.
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

        ////Demonskin_Mitts,
        //Dimensional_Blade,
        //Elegant_blade,
        //Falchion,

        //Espadon,
        //Executioner_Sword,
        //Flamberge,

        // Dirk,
        //Fanged_knife,

        //Divine_Scepter,

        //Ettin_axe,

        //Double_Axe,

        //     Eagle_Crown,
        //Earth_spirit,


        //Dragon_Shield,
        //Dream_Spirit,


        //Elder_staff,

        //Eldritch_orb,
        //Dusk_shroud,

        //Elf_Wood_Bow,

        //Embossed_Plate,
        //Field_Plate,

        //Fanged_Helm,

        ////Endless_Reaper,

        //    //spear
        //Ethereal_Spear,
        //Fascia,

        //// claw
        //Feral_claws,


        //Flail,
        //Flanged_Mace,
        //Flay_Scissors,
        //Flying_axe,
        //Francisca,
        //Frenzy_Axe,
        //Full_Helm,
        //Full_Plate_Mail,
        //Fullmoon_Visor,
        //Fury_visor,
        //Fuscina,
        //Gauntlets,
        //Gemmed_Cap,
        //Ghost_Armor,
        //Ghost_glaive,
        //Giant_Axe,
        //Giant_Ogre_Axe,
        //Giant_Ogre_Glaive,
        //Giant_Ogre_Maul,
        //Giant_Sword,
        //Giant_Thresher,
        //Giants_Spear,
        //Girdle,
        //Gladius,
        //Glorious_axe,
        //Gloves,
        //Gnarled_Staff,
        //Gothic_Axe,
        //Gothic_Bow,
        //Gothic_Plate,
        //Gothic_Shield,
        //Gothic_Staff,
        //Gothic_Sword,
        //Grand_Amazonian_Caster,
        //Grand_Crown,
        //Grand_Scepter,
        //Grave_Wand,
        //Great_Axe,
        //Great_Helm,
        //Great_maul,
        //Great_Maul,
        //Great_Sword,
        //Grim_Helm,
        private void UpdateSubCategory()
            => SubCategory =
                    new[]
                    {
                        ItemType.Archon_Edge_Blade,
                        ItemType.Ataghan,
                        ItemType.Bastard_Sword,
                        ItemType.Battle_Sword,
                        ItemType.Blade,
                        ItemType.Brawler_Blade,
                        ItemType.Cryptic_sword,
                        ItemType.Crystal_Sword,
                        ItemType.Dimensional_Blade,


                    }.Contains(Type) ? ItemSubCategory.Sword :
                    new[]
                    {
                        ItemType.Ancient_Sword,
                        ItemType.Balrog_blade,
                        ItemType.Broad_Sword,
                        ItemType.Champion_Sword,
                        ItemType.Claymore,
                        ItemType.Colossus_Blade,
                        ItemType.Dacian_Falx,
                    }.Contains(Type) ? ItemSubCategory.TwoHandedSword :
                    new[]
                    {
                        ItemType.Angel_String_Bow,
                        ItemType.Carnage_Bow,
                        ItemType.CedarBow,
                        ItemType.Ceremonial_Bow,
                        ItemType.Composite_Bow,
                        ItemType.Crusader_Bow,
                    }.Contains(Type) ? ItemSubCategory.Bow :
                    new[]
                    {
                        ItemType.Arbalest,
                        ItemType.Balista,
                        ItemType.Chu_Ko_Nu,
                        ItemType.Colossus_crossbow,
                        ItemType.Dark_String_Crossbow,
                        ItemType.Demon_crossbow,
                        ItemType.Edge_Bow,
                        ItemType.Double_Bow,
                        ItemType.Double_String_Bow,
                        ItemType.Elf_Wood_Bow,
                    }.Contains(Type) ? ItemSubCategory.Crossbow :
                    new[]
                    {
                        ItemType.Axe,
                        ItemType.Hand_Axe,
                        ItemType.Balanced_Crescent,
                        ItemType.Balanced_Tomahawk,
                        ItemType.Barbarians_Edge,
                        ItemType.Bearded_Axe,
                        ItemType.Berserker_axe,
                        ItemType.Cleaver,


                    }.Contains(Type) ? ItemSubCategory.Axe :
                    new[]
                    {
                        ItemType.Ancient_Axe,
                        ItemType.Bearded_Axe,
                        ItemType.Bec_de_Corbin,
                        ItemType.Champion_Axe,
                        ItemType.Cryptic_axe,
                        ItemType.Decapitator,
                    }.Contains(Type) ? ItemSubCategory.AxeWeapon :
                    new[]
                    {
                        ItemType.Battle_cestus,
                        ItemType.Cestus,
                    }.Contains(Type) ? ItemSubCategory.Claw :
                    new[]
                    {
                        ItemType.Demon_Hook_Pilum,
                    }.Contains(Type) ? ItemSubCategory.ThrowWeapon :
                    new[]
                    {
                        ItemType.Barbed_Club,
                        ItemType.Club,
                        ItemType.Crowbill,
                        ItemType.Devil_Star,
                        ItemType.Diamond_Edged_Mace,
                    }.Contains(Type) ? ItemSubCategory.Masse :
                    new[]
                    {
                        ItemType.Amazonian_Matron_Pike,
                        ItemType.Amazonian_Matron_Spear,
                        ItemType.Amazonian_Piercers,
                        ItemType.Balrog_spear,
                        ItemType.Battle_Hammer,
                        ItemType.Ceremonial_Pike,
                        ItemType.Council_Spike,
                        ItemType.Council_War_Spike,
                        ItemType.Diamon_Tip_Spear,

                    }.Contains(Type) ? ItemSubCategory.Lance :
                    new[]
                    {
                        ItemType.Double_Axe,
                        ItemType.War_Axe,
                        ItemType.Military_Pick,
                        ItemType.Balanced_Mithril_Piercer,
                        ItemType.Bardiche,
                        ItemType.Brandistock,
                        ItemType.Broad_Axe,
                        ItemType.Champion_Axe,
                    }.Contains(Type) ? ItemSubCategory.AxeWeapon :
                    new[]
                    {
                        ItemType.Blessed_Scepter,
                        ItemType.Caduceus,
                        ItemType.Cudgel,
                    }.Contains(Type) ? ItemSubCategory.Scepter :
                    new[]
                    {
                        ItemType.Bone_Knife,
                        ItemType.Cinquedeas,
                        ItemType.Cutlass,
                        ItemType.Dagger,
                        ItemType.Demon_Bone_Knife,
                    }.Contains(Type) ? ItemSubCategory.Dagger :
                    new[]
                    {
                        ItemType.Battle_Scythe,
                    }.Contains(Type) ? ItemSubCategory.Scythe :
                    new[]
                    {
                        ItemType.Acrhon_Orb,
                        ItemType.Arcanist_Stick,
                        ItemType.Arcanite_Stick,
                        ItemType.Archon_Soul_Shard,
                        ItemType.Bone_Wand,
                        ItemType.Burnt_Wand,
                        ItemType.Dark_Summoner_Wand,
                        ItemType.Dimensional_shard,
                        ItemType.Dimensional_shard,

                    }.Contains(Type) ? ItemSubCategory.Wand :
                    new[]
                    {
                        ItemType.Archon_staff,
                        ItemType.Battle_Staff,
                        ItemType.Cedar_Staff,
                        ItemType.Dark_Archon_Staff,
                    }.Contains(Type) ? ItemSubCategory.Staff :
                    new[]
                    {
                        ItemType.Ceremonial_Javelin,
                    }.Contains(Type) ? ItemSubCategory.Javelin :
                    new[]
                    {
                        ItemType.Aegis,
                        ItemType.Aerin_Shield,
                        ItemType.Ancient_Shield,
                        ItemType.Barbed_Shield,
                        ItemType.Blade_barrier,
                        ItemType.Bone_Shield,
                        ItemType.Buckler,
                        ItemType.Crown_Shield,
                        ItemType.Defender,
                        ItemType.Demon_Ward,
                    }.Contains(Type) ? ItemSubCategory.Shield :
                    new[]
                    {
                        ItemType.Demonhead,
                    }.Contains(Type) ? ItemSubCategory.NecroShield :
                    new[]
                    {
                        ItemType.Amazonian_Girdle,
                        ItemType.Arcanic_Sash,
                        ItemType.Battle_Belt,
                        ItemType.Belt,
                        ItemType.Cursed_Belt,
                        ItemType.Demonhide_Sash,
                        ItemType.Demonskin_Belt,
                    }.Contains(Type) ? ItemSubCategory.Belt :
                    new[]
                    {
                        ItemType.Amazonian_Hauberk,
                        ItemType.Amazonian_Plate,
                        ItemType.AncientArmor,
                        ItemType.Arcanic_Shroud,
                        ItemType.Balrog_Skin,
                        ItemType.Breast_Plate,
                        ItemType.Chain_Mail,
                        ItemType.Chaos_Armor,
                        ItemType.Cuirass,
                        ItemType.Cursed_Hide,
                        ItemType.Dark_Archon_Plate,
                        ItemType.Demon_Plate,
                        ItemType.Demonhide_Armor,

                    }.Contains(Type) ? ItemSubCategory.Armor :
                    new[]
                    {
                       ItemType.Arcanic_Mitts,
                       ItemType.Battle_Guantlets,
                       ItemType.Bracers,
                       ItemType.Crusader_Gauntlets,
                       ItemType.Cursed_Gloves,
                       ItemType.Demonhide_Gloves,
                       ItemType.Diamondweave_Mesh,

                    }.Contains(Type) ? ItemSubCategory.Glove :
                    new[]
                    {
                        ItemType.Arcanic_Boots,
                        ItemType.Battle_Boots,
                        ItemType.Boneweave_boots,
                        ItemType.Chain_Boots,
                        ItemType.Cursed_Boots,
                        ItemType.Demonhide_Boots,
                        ItemType.Demonskin_Boots,


                    }.Contains(Type) ? ItemSubCategory.Boot :
                    new[]
                    {
                        ItemType.Amazonian_Tiara,
                        ItemType.Arcanic_Circlet,
                        ItemType.Armet,
                        ItemType.Assault_Helmet,
                        ItemType.Balrog_Skull,
                        ItemType.Basinet,

                        ItemType.Bloodlord_skull,
                        ItemType.Bone_Helm,
                        ItemType.Bone_visage,
                        ItemType.Cap,
                        ItemType.Casque,
                        ItemType.Conqueror_crown,
                        ItemType.Corona,
                        ItemType.Crown,
                        ItemType.Cursed_Demon_Skull,
                        ItemType.Cursed_Mask,
                        ItemType.Cursed_Visage,
                        ItemType.Death_Mask,
                        ItemType.Demon_Visor,
                        ItemType.Destroyer_helm,
                        ItemType.Diadem,

                    }.Contains(Type) ? ItemSubCategory.Helm :
                    new[]
                    {
                        ItemType.Blood_spirit,
                        ItemType.Direwolf_Helm,
                        ItemType.Direwolf_Helm,
                        ItemType.Druid_Helm,
                        ItemType.Eagle_Crown,
                        ItemType.Earth_spirit,
                    }.Contains(Type) ? ItemSubCategory.DruidHelm :
                    new[]
                    {
                        ItemType.Amulet,
                    }.Contains(Type) ? ItemSubCategory.Amulet :
                    new[]
                    {
                        ItemType.Arrows,
                        ItemType.Bolts,
                    }.Contains(Type) ? ItemSubCategory.Arrow :
                    new[]
                    {
                        ItemType.Charm,
                    }.Contains(Type) ? ItemSubCategory.Charm :
                    ItemSubCategory.UNKNOWN;
    }
}