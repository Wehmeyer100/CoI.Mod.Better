# Captain of Industry mod: Better

Game Version(Comptability): 0.3.10

Savegame Comptability: You need to start a new game!

# Current overview of the functions:
- Void Destroyer - Shreds products(no liquids, no losse), has as output stones. and causes emissions.
- All vanilla storages have 3x more space. 
- The liquid stores have 30x more than the vanilla building.
- The refugee system has been overhauled so that refugees are now arriving forever. However, the rhythm has been changed, the number of refugees varies, the gift amounts are now also randomly generated and it can also happen that you do not receive any gifts. The number of refugees and the gifts increase at the beginning and stop increasing after a certain limit, but these can also fluctuate strongly.
- More edict have been added that offer your load amount of trucks, their consumption and their maintenance more slopes.
- You can now increase your vehicle limit with further research. However, it may happen that the performance suffers. 
- The range of your mining tower has been increased by 50%.
- Edit various settings in the Config. It should be noted that this is created only after the load / new game. The config is valid for all new and old savegames. So be careful what you change.

# Cheats:

- Cheats can be disabled in the config file.

Machine:
- Void Destroyer - Shreds products(no liquids, no losse), has no waste and also no emissions.
- Void Producer Liquids - You can produce all liquids
- Void Producer Products - You can produce all Products
- Void Producer Loose - You can produce all Loose
- Diesel Generators - 1 Diesel per minute, produce: 10MW, 50MW, 100MW, 200MW, 1GW

Research:
- Vehicle Capacity +250

Edicts: 
- Maintenance reduce: -30%, -40%, -50%, -60%
- Fuel consume reduce: -25%, -50%, -75%, -95%
- Truck load size increase: 100%, 200%, 300%, 400%, 500%
- Unity Plus(increase unity produce)): 5%, 10%, 20%, 50%, 100%
- Reduce all Services: -30%, -40%, -50%, -60%, -75%
# Bugs
- The new storage size is not displayed in the research tree. However, after researching the building has the modded size.

# Install
Download this file: https://github.com/Wehmeyer100/CoI.Mod.Better/releases/tag/0.1.6

1. Open your documents folder. Here you should find the "CaptainOfIndustry" folder. 

2. Is the "Mods" folder present? If not, just create a new one with the name "Mods".

3. Copy the folder "CoI.Mod.Better" and paste it into the "Documents\CaptainOfIndustry\Mods" folder. The folder name must be exact "CoI.Mod.Better"!

4. Start a new game!

Then start the game and enjoy!

# FAQ

1. May I rename the folder or file ? 
    No, CoI would otherwise have problems and find the mod.

2. Why is the mod not loaded?
    There are several reasons.
    1. in the settings menu the option Mods was not activated.
    2. is the file/mod path correct? It must look like this: "Documents\CaptainOfIndustry\Mods\CoI.Mod.Better\CoI.Mod.Better.dll".

3. How do I see that the mod is active?
    Here, too, there are several possibilities.
    
    Log Folder: "Documents\CaptainOfIndustry\Logs"
    
    1. You look in the last log and in the log should be the following to find: 
     
        Game will start with following 4 mods:
        
        Mafi.Core.CoreMod
        
        Mafi.Base.BaseMod
        
        Mafi.Unity.UnityMod
        
        CoI.Mod.Better.MoreRecipes <--- If this is displayed, the mod has been loaded.
        
        
    2. In the research tree there should be new research options at the top.

# Config

    "DisableBigStorage": false,                 => Disables the modded size of the warehouses., Only for new game!!!

    "DisableGenerellEdicts": false,             => Deactivates the added generell edicts, Only for new game!!!
    
    "DisableVehicleEdicts": false,              => Deactivates the added Vehicle edicts, Only for new game!!!
    
    "DisableNewRefugeesSystem": false,          => Deactivates the newly Refugees System, Only for new game!!!
    
    "DisableExtentedMineTowerRange": false,     => Deactivates the modded range of the Mining Tower, Only for new game!!!
    
    "DisableVehicleCapIncrease": false,         => Disables the modded VehicleCapacity., Only for new game!!!
    
    "DisableVoidCrusher": false,                => Disables the Void Crusher, Only for new game!!!
    
    "DisableCheats": false,                     => Disables the Cheats, Only for new game!!!

    "DisableVoidProducer": false,               => Disables VoidProducer, Only for new game!!!

    "DisableDieselGeneators": false,            => Disables DieselGeneators, Only for new game!!!
    
    "BeaconRefugeesMin": 1,
    
    "BeaconRefugeesMax": 20,
    
    "BeaconDurationMin": 2,
    
    "BeaconDurationMax": 6,

    "BeaconRewardBaseValueMultiplier": 1.0,

    "BeaconRewardIronBaseValue": 1.5,

    "BeaconRewardIronChance": 1.0,

    "BeaconRewardCopperBaseValue": 1.0,

    "BeaconRewardCopperChance": 0.8999999761581421,

    "BeaconRewardRubberBaseValue": 0.75,

    "BeaconRewardRubberChance": 0.800000011920929,

    "BeaconRewardOilBaseValue": 0.5,

    "BeaconRewardOilChance": 0.4000000059604645,

    "BeaconRewardDieselBaseValue": 0.25,

    "BeaconRewardDieselChance": 0.4000000059604645,

    "BeaconRewardFoodBaseValue": 1.0,

    "BeaconRewardFoodChance": 0.699999988079071,
    
    "TowerAreaMultiplier": 1.5,
    
    "StorageCapacitySmall": 540,
    
    "StorageCapacityLarge": 1080,
    
    "FluidStorageCapacityMultiplier": 10,
    
    "NuclearWasteStorageCapacityMultiplier": 5,
    
    "CheatUpkeepEdicts": -0.5,
    
    "VehicleEdictsResearchCostT1": 9,
    
    "VehicleEdictsResearchCostT2": 12,
    
    "VehicleEdictsResearchCostT3": 15,
    
    "VehicleEdictsResearchCostT4": 18,
    
    "VehicleEdictsResearchCostT5": 21,
    
    "VoidDestroyCheatPowerConsume": 75,
    
    "VoidDestroyCheatAmountInput": 40,
    
    "VoidDestroyCheatDuration": 20,
    
    "VoidDestroyCheatEmission": 0,

    "VoidProducerCheatPowerConsume": 75,

    "VoidProducerCheatAmountInput": 40,

    "VoidProducerCheatDuration": 20,

    "VoidProducerEnergyInputType": 1, # 1 = Diesel, 2 = Water, 3 = Rohöl,

    "VoidProducerEnergy10CheatInKW": 10000,

    "VoidProducerEnergy50CheatInKW": 50000,

    "VoidProducerEnergy100CheatInKW": 100000,

    "VoidProducerEnergy200CheatInKW": 200000,

    "VoidProducerEnergy1000CheatInKW": 1000000,

    "VoidProducerEnergy10CheatBufferCapactiy": 540,

    "VoidProducerEnergy50CheatBufferCapactiy": 1080,

    "VoidProducerEnergy100CheatBufferCapactiy": 1620,

    "VoidProducerEnergy200CheatBufferCapactiy": 2160,

    "VoidProducerEnergy1000CheatBufferCapactiy": 2700
    

# 
I wish you a lot of fun!
Luxgor
