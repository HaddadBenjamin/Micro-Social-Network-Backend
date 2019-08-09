export default interface SearchItemDto
{
    SubCategories? : ItemSubCategory[],
}

export const DefaultSearchItemDto : SearchItemDto =
{
    SubCategories: [],
}

enum ItemSubCategory
{
    Amazon_Bow = "Amazon_Bow",
    Amazon_Javelin = "Amazon_Javelin",
    Amazon_Spear  = "Amazon_Spear",
    Amulet = "Amulet",
    Arrows = "Arrows",
    Assassin_Claw = "Assassin_Claw",
    Axe = "Axe",
    Barbarian_Helm = "Barbarian_Helm",
    Bolts = "Bolts",
    Bow = "Bow",
    Charm = "Charm",
    Club = "Club",
    Crossbow = "Crossbow",
    Druid_Helm = "Druid_Helm",
    Feet = "Feet",
    Hammer = "Hammer",
    Hand_To_Hand = "Hand_To_Hand",
    Hand_To_Hand_Two_Handed = "Hand_To_Hand_Two_Handed",
    Hands = "Hands",
    Head = "Head",
    Javelin = "Javelin",
    Jewel = "Jewel",
    Knife = "Knife",
    Mace = "Mace",
    Necromancer_Shield = "Necromancer_Shield",
    Offhand = "Offhand",
    Orb = "Orb",
    Paladin_Shield = "Paladin_Shield",
    Polearm = "Polearm",
    Ring = "Ring",
    Scepter = "Scepter",
    Sorceress_Orb = "Sorceress_Orb",
    Spear = "Spear",
    Staff = "Staff",
    Sword = "Sword",
    Thorwing_Knife = "Thorwing_Knife",
    Throwing_Axe = "Throwing_Axe",
    Throwing_Potions = "Throwing_Potions",
    Torso = "Torso",
    Two_And_One_Handed_Sword = "Two_And_One_Handed_Sword",
    Two_Handed_Amazon_Bow = "Two_Handed_Amazon_Bow",
    Two_Handed_Amazon_Spear = "Two_Handed_Amazon_Spear",
    Two_Handed_Axe = "Two_Handed_Axe",
    Two_Handed_Bow = "Two_Handed_Bow",
    Two_Handed_Crossbow = "Two_Handed_Crossbow",
    Two_Handed_Hammer = "Two_Handed_Hammer",
    Two_Handed_Polearm = "Two_Handed_Polearm",
    Two_Handed_Spear = "Two_Handed_Spear",
    Two_Handed_Staff = "Two_Handed_Staff",
    Waist = "Waist",
    Wand = "Wand"
}

export { ItemSubCategory }