using DnDGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDGenerator.StaticCollections
{
    public enum Alignment : Int16
    {
        ChaoticGood = 1,
        LawfulGood = 2,
        NeutralGood = 3,
        ChaoticNeutral = 4,
        LawfulNeutral = 5,
        Neutral = 6,
        ChaoticEvil = 7,
        LawfulEvil = 8,
        NeutralEvil = 9
    };

    public enum Biome : Int16
    {
        Temperate = 1,
        Tropical = 2,
        Frigid = 3,
        Arid = 4,
        Underground = 5
    };

    public enum TileSubType : Int16
    {
        Village = 1,
        Keep = 2,
        City = 3,
        //Metropolis = 4,
        Forest = 4,
        Prairie = 5,
        Desert = 6,
        Tundra = 7,
        //Ocean = 8,  implement this and metropolis later!
        Mountain = 8
    };

    public enum DungeonType : Int16
    {
        Natural_Cave = 1,
        Necromancer_Hideout = 2,
        Ancient_Tomb = 3,
        Demon_Lair = 4,
        Cult_Hideout = 5,
        Bandit_Base = 6,
        Underdark_Entrance = 7,
        Wizard_Lair = 8,
        Dragon_Lair = 9,
        Aboleth_Pool = 10,
        MindFlayer_Lair = 11,
        Mad_Machinist_Lair = 12,
        Humanoid_Stronghold = 13,
        Cave_Druid_Glen = 14
    }

    public enum RuinType : Int16
    {
        Small_Abandoned_Keep = 1,
        Large_Abandoned_Keep = 2,
        Small_Abandoned_Temple = 3,
        Large_Abandoned_Temple = 4,
        Small_Abandoned_Mage_Tower = 5,
        Large_Abandoned_Mage_Tower = 6,
        Small_Abandoned_Workshop = 7,
        Large_Abandoned_Workshop = 8,
        Abandoned_Genetic_Experimentation_Lab = 9,
        Abandoned_Hades_Gate = 90,
        Abandoned_Infernal_Gate = 91,
        Abandoned_Celestial_Gate = 92,
        Abandoned_Many_Doors_Gate = 93,
        Abandoned_Arid_Gate = 94, // Gate to a region to an arid portion of the game world
        Abandoned_Frigid_Gate = 95, 
        Abandoned_Tropical_Gate = 96, 
        Abandoned_Temperate_Gate = 97, 
        Abandoned_Ethereal_Gate = 98
    }

    public enum Frequency : Int16
    {
        Very_Rare = 1,
        Rare = 2,
        Seldom = 3,
        Frequent = 4,
        Often = 5
    }

    public enum SettlementCondition : Int16
    {
        Missing_Persons = 1, // Abductions or murders!
        Missing_Livestock = 2, // The work of rustlers or beasts!
        Celebrations = 3, // Perhaps a holy day
        Pestilence = 4, // The work of a nefarious mage, or perhaps a natural malady
        Drought = 5, // Impairs harvests
        Food_Shortage = 6, // Effects food prices
        Import_Shortage = 7, // Raises export prices and import prices
        Export_Glut = 8, // The cure for high supply is low prices!
        Ambushed_Trade_routes = 9, // 
        Happy_Mood = 10, // Better prices
        Suspicious_Mood = 11,
        Raided = 12, // Bad moods and worse prices as a result of disruption
        Mustering = 13, // Raising troops to fight a war, labor and weapons are in high demand! Be wary of press-ganging.
        Devastated = 14, // Whether from invasion or a wondering beast
        Terrified = 15, // As suspicious_mood, but worse. Often the result of a regional threat or supernatural beast
        Bountiful_Harvest = 16 // Food and mead flow freely
    }

    public enum WildernessEffects : Int16
    {
        Raiding_Party = 1,
        Druid_Conclave = 2,
        Merchants = 3,
        Adventurers = 4,
        Reality_Rift_Fae = 5,
        Reality_Rift_Elemental = 6,
        Reality_Rift_Infernal = 7,
        Reality_Rift_Celestial = 8,
        Ranger_Expedition = 9,
        Wizard_Questing = 10,
        Wandering_Predator = 11
    }

    public enum WildernessFeature : Int16
    {
        Trappers = 1, // NPCs to trade with
        Refreshing_Glen = 2, // Healing herbs and a good spot to safely rest 
        Druid_Garden = 3, // A powerful NPC Druid
        Wizard_Keep = 4, // A powerful NPC wizard
        Deep_Pool = 5, // Adds aquatic monsters
        Marsh = 6, // Adds marsh/bog/swamp monsters
        Ranger_Outpost = 7
    }

    public enum OrganizedCrime : Int16
    {
        Bandit_Camp = 1,
        Slavers = 2,
        Vampire_Coven = 3
    }

    public enum RegionalThreats : Int16
    {
        Underdark_Humanoid_Invasion = 1,
        Underdark_Predator = 2,
        Dragon = 3,
        Infernal_Invasion = 4,
        Aboleth_Invasion = 5,
        Humanoid_Invasion_Within_Region = 6,
        Humanoid_Invasion_Outside_Region = 7,
        Behemoth = 8,
        Predator = 9,
        Necromancer = 10,
        Druid = 11,
        Wizard = 12,
        Mechanic = 13,
        Cleric = 14,
        Drought = 15,
        Plague = 16, // Add pestilence monster den
        Chronic_Raiding = 17,
        Chronic_Crime = 18
    }

    public enum Commodities : Int16
    {
        Grains = 1,
        Ores = 2,
        Conscripts = 3,
        Holy_Site = 4, // Not a commodity, but it is an industry...
        Ceramics = 5,
        Incense = 6,
        Metal_Tools = 7,
        Weapons = 8,
        Furs = 9,
        Herbs = 10,
        Mana_Crystals = 11,
        Mechanical_Tools = 12,
        Trained_Wizards = 13,
        Trained_Priests = 14,
        Mercenaries = 15,
        Magical_Weapons = 16,
        Magical_Armor = 17,
        War_Machines = 18,
        Autonomous_Robots = 19,
        Modern_Weapons = 20, // Ships and devices for planar travel
        Livestock = 21,
        Poisons = 22,
        Assassins = 23,
        Fine_Clothes = 24,
        Medicines = 25,
        Wine = 26,
        Horses = 27,
        Trained_Beasts = 28,
        Scientists = 29, // Like wizards, assassins, and priests, these are a source of human capital that may be useful
        Prostitutes = 30,
        Trained_Solders = 31,
        Preserved_Foods = 32,
        Linen = 33,
        Politicians = 33,
        Music_Arts = 34,
        Potions = 35
    }
    public enum EnvironmentTag : short
    {
        Forest = 1,
        Desert = 2,
        Tundra = 3,
        Mountain = 4,
        Underground = 5,
        Urban = 6,
        Aquatic = 7,
        Marsh = 8,
        Ocean = 9,
        Common = 99
    }

    public enum CreatureTraitTag : short
    {
        Aasimar = 1,
        Aberration = 2,
        Acid = 3,
        Aeon = 4,
        Aesir = 5,
        Agathion = 6,
        Air = 7,
        Alchemical = 8,
        Amphibious = 9,
        Anadi = 10,
        Android = 11,
        Angel = 12,
        Animal = 13,
        Anugobu = 14,
        Aphorite = 15,
        Aquatic = 16,
        Arcane = 17,
        Archon = 18,
        Ardande = 19,
        Astral = 20,
        Asura = 21,
        Athamaru = 22,
        Automaton = 23,
        Azarketi = 24,
        Azata = 25,
        Beast = 26,
        Blight = 27,
        Boggard = 28,
        Bugbear = 29,
        Caligni = 30,
        Celestial = 31,
        Changeling = 32,
        Charauka = 33,
        Clockwork = 34,
        Coatl = 35,
        Cold = 36,
        Construct = 37,
        Couatl = 38,
        Daemon = 39,
        Darvakka = 40,
        Demon = 41,
        Dero = 42,
        Devil = 43,
        Dinosaur = 44,
        Div = 45,
        Divine = 46,
        Dragon = 47,
        Dream = 48,
        Drow = 49,
        Duergar = 50,
        Dwarf = 51,
        Earth = 52,
        Eidolon = 53,
        Electricity = 54,
        Elemental = 55,
        Elf = 56,
        Ethereal = 57,
        Evil = 58,
        Evocation = 59,
        Fetchling = 60,
        Fey = 61,
        Fiend = 62,
        Fire = 63,
        Force = 64,
        Formian = 65,
        Fungus = 66,
        Ganzi = 67,
        Genie = 68,
        Ghoran = 69,
        Ghost = 70,
        Ghoul = 71,
        Ghul = 72,
        Giant = 73,
        Gnoll = 74,
        Gnome = 75,
        Goblin = 76,
        Golem = 77,
        Graveknight = 78,
        Gremlin = 79,
        Grioth = 80,
        Grippli = 81,
        Hag = 82,
        Halfling = 83,
        Hantu = 84,
        Herald = 85,
        Hobgoblin = 86,
        Holy = 87,
        Hryngar = 88,
        Human = 89,
        Humanoid = 90,
        Ifrit = 91,
        Ikeshti = 92,
        Illusion = 93,
        Incorporeal = 94,
        Inevitable = 95,
        Kaiju = 96,
        Kami = 97,
        Kashrishi = 98,
        Kholo = 99,
        Kitsune = 100,
        Kobold = 101,
        Kovintus = 102,
        Leshy = 103,
        Light = 104,
        Lilu = 105,
        Lizardfolk = 106,
        Mental = 107,
        Merfolk = 108,
        Metal = 109,
        Mindless = 110,
        Minion = 111,
        Monitor = 112,
        Morlock = 113,
        Mortic = 114,
        Mummy = 115,
        Munavri = 116,
        Mutant = 117,
        Mythic = 118,
        Nagaji = 119,
        Necromancy = 120,
        Negative = 121,
        Nindoru = 122,
        Nymph = 123,
        Occult = 124,
        Oni = 125,
        Ooze = 126,
        Orc = 127,
        Oread = 128,
        Paaridar = 129,
        Palinthanos = 130,
        Petitioner = 131,
        Phantom = 132,
        Plant = 133,
        Poison = 134,
        Positive = 135,
        Primal = 136,
        Protean = 137,
        Psychopomp = 138,
        Qlippoth = 139,
        Rakshasa = 140,
        Rare = 141,
        Ratajin = 142,
        Ratfolk = 143,
        Sahkil = 144,
        Samsaran = 145,
        SeaDevil = 146,
        Sedacthy = 147,
        Serpentfolk = 148,
        Seugathi = 149,
        Shabti = 150,
        Shadow = 151,
        Shapechanger = 152,
        Shoony = 153,
        Siktempora = 154,
        Skeleton = 155,
        Skelm = 156,
        Skulk = 157,
        Soulbound = 158,
        Spirit = 159,
        Sporeborn = 160,
        Spriggan = 161,
        Sprite = 162,
        Stheno = 163,
        Strix = 164,
        Suli = 165,
        Swarm = 166,
        Sylph = 167,
        Talos = 168,
        Tane = 169,
        Tanggal = 170,
        Time = 171,
        Titan = 172,
        Troll = 173,
        Troop = 174,
        Uncommon = 175,
        Undead = 176,
        Undine = 177,
        Unholy = 178,
        Unique = 179,
        Urdefhan = 180,
        Vampire = 181,
        Vanara = 182,
        Velstrac = 183,
        Vishkanya = 184,
        Void = 185,
        Water = 186,
        Wayang = 187,
        Werecreature = 188,
        Wight = 189,
        Wild_Hunt = 190,
        Wood = 191,
        Wraith = 192,
        Wraithvine = 193,
        Wyrwood = 194,
        Xulgath = 195,
        Zombie = 196,
        Pestilence = 197 // Spreaders of pestilence. Does not exist in Pathfinder keywords so will only return homebrew monsters :P 

        /*
        Humanoid = 1,
        Undead = 2,
        Dragon = 3,
        Elemental = 4,
        Fae = 5,
        Infernal = 6,
        Celestial = 7,
        Beast = 8,
        Aberration = 9,
        Psychic = 10,
        Construct = 11,
        Monstrosity = 12,
        Mutant = 13,
        Druid = 14,
        Plant = 15,
        Wizard = 16,
        Necromancer = 17,
        Pestilence = 18 // Spreaders of pestilence
        */
    }

    public enum CreatureFamilyTag : short
    {
        Orc = 1,
        Elf = 2,
        Goblin = 3,
        Gnome = 4,
        Calgiri = 5,
        Aboleth = 6,
        Mind_Flayer = 7,
        Beast = 8,
        Aberration = 9,
        Serpentfolk = 10,

    }

    public enum PredationTag : byte
    {
        Herbivore = 0000_0000_0001,
        SmallPredator = 0000_0000_0010,
        LargePredator = 0000_0000_0100,
        Humanoid = 0000_0000_0200
    }

    public enum Domain : short
    {
        Death = 1,
        Life = 2,
        Fertility = 3,
        Destruction = 4,
        Good = 5,
        Healing = 6,
        Sun = 7,
        Love = 8,
        Agriculture = 9,
        Evil = 10,
        War = 11,
        Strength = 12,
        Heroism = 13,
        Beasts = 14,

    }

    public enum Profession : short // Class from Pathfinder, but with commoner, artisan, et thrown in. 
    {
        Rogue = 1,
        Wizard = 2,
        Cleric = 3,
        Fighter = 4,
        Artisan = 5,
        Commoner = 6,
        Monster = 7
    }

    public enum Race : short // Will need to be a class eventually
    {
        Tiefling = 1,
        Human = 2,
        Elf = 2,
        Orc = 3,
        Half_Orc = 4,
        Gnome = 5,
        Dwarf = 6,
        Catfolk = 7,
        Serpentfolk = 8,
        Dragonborne = 9,
        Halfling = 10,
        ElderEvil = 98,
        God = 99
    }

    public enum Rarity : short
    {
        Common = 1,
        Uncommon = 2,
        Rare = 3,
        Unique = 4
    }

    public enum Size : short
    {
        Tiny = 1,
        Small = 2,
        Medium = 3,
        Large = 4,
        Huge = 5,
        Gargatuan = 6
    }

}
