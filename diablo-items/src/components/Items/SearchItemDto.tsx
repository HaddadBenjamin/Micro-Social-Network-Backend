export default interface SearchItemDto
{
    SubCategories? : ItemSubCategory[],
    MinimumLevelRequired : number,
    MaximumLevelRequired : number,
}

export const DefaultSearchItemDto : SearchItemDto =
{
    SubCategories: [],
    MinimumLevelRequired: 0,
    MaximumLevelRequired: Math.max()
}

enum ItemSubCategory
{
    Amazon_Bow,
    Amazon_Javelin,
    Amazon_Spear,
    Amulet,
    Arrows,
    Axe,
    Barbarian_Helm,
    Bolts,
    Bow,
    Charm,
    Club,
    Crossbow,
    Druid_Helm,
    Feet,
    Hammer,
    Hand_To_Hand,
    Hand_To_Hand_Two_Handed,
    Hands,
    Head,
    Javelin,
    Jewel,
    Knife,
    Mace,
    Offhand,
    Orb,
    Polearm,
    Ring,
    Scepter,
    Spear,
    Staff,
    Sword,
    Thorwing_Knife,
    Throwing_Axe,
    Throwing_Potions,
    Torso,
    Two_And_One_Handed_Sword,
    Two_Handed_Amazon_Bow,
    Two_Handed_Amazon_Spear,
    Two_Handed_Axe,
    Two_Handed_Bow,
    Two_Handed_Crossbow,
    Two_Handed_Hammer,
    Two_Handed_Polearm,
    Two_Handed_Spear,
    Two_Handed_Staff,
    Waist,
    Wand
}

export { ItemSubCategory }