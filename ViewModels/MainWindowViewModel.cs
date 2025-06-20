using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DnDGenerator.Models;
using System;
using FastJson = System.Text.Json.JsonSerializer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace DnDGenerator.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Region>? regions;
        [ObservableProperty]
        private Region? selectedRegion;
        [ObservableProperty]
        private ObservableCollection<Tile>? tiles;
        [ObservableProperty]
        private ObservableCollection<Tile>? tilesSorted;

        [ObservableProperty]
        private Tile? selectedTile; 

        [RelayCommand]
        public void GenerateRegion()
        {
            if(Regions is null)
            {
                Regions = new();
            }
            Regions.Add(new());
        }

        // Probably should make async
        partial void OnSelectedRegionChanged(Region? value)
        {
            if(value.TilesList is not null)
            {
                Tiles = new(value!.TilesList);

                TilesSorted = new();

                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        TilesSorted.Add(Tiles.Where(z => z.Lat == x && z.Lon == y).First());
                    }
                }
            }
        }

        [RelayCommand]
        public async Task ExportRegion() // remove before upload
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };


            string finalPath = "C:\\Users\\fuzzy\\Downloads\\region.json";

            if (selectedRegion is not null)
            {
                using (StreamWriter jsonStream = File.CreateText(finalPath))
                {
                    jsonStream.Write(System.Text.Json.JsonSerializer.Serialize(selectedRegion, options));
                }
            }
        }

            [RelayCommand]
        public async Task ImportRegion() 
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            string jsonPath = "C:\\Users\\fuzzy\\Downloads\\region.json"; // remove before upload
            await using (FileStream jsonLoad = File.Open(jsonPath, FileMode.Open))
            {
                // Deserialize object graph into a List of Person
                Region? region = await System.Text.Json.JsonSerializer.DeserializeAsync(utf8Json: jsonLoad, returnType: typeof(Region), options: options) as Region;
                
                if(Regions is null)
                {
                    Regions = new();
                }
                if(region is not null)
                {
                    foreach (Tile tile in region.TilesList)
                    {
                        tile.SetNeighbors(region.TilesList);
                    }
                    Regions.Add(region);
                }
            }
        }
    }

}
