using Mimeo.DynamicUI.Avalonia.ViewModels;
using Mimeo.DynamicUI.Data;
using Mimeo.DynamicUI.Demo.Shared.Models;
using Mimeo.DynamicUI.Demo.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mimeo.DynamicUI.Demo.Avalonia
{
    public class MainWindowViewModel
    {        
        public MainWindowViewModel(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException();
            CreateTestData();
            DynamicEditFormViewModel = new TestViewModel(inMemorySampleDatabase.First(), new DataService());
        }

        private readonly object inMemoryLock = new();
        private readonly List<TestModel> inMemorySampleDatabase = [];

        public IServiceProvider ServiceProvider { get; }

        public ViewModel DynamicEditFormViewModel { get; set; }

        public MainViewModel Test => new();

        private void CreateTestData()
        {
            for (int i = 0; i < 100; i++)
            {
                inMemorySampleDatabase.Add(new TestModel
                {
                    Id = Guid.NewGuid(),
                    Name = $"Item {i}",
                    Description = "A sample item",
                    HTML = "<p>Some HTML content</p>",
                    JSON = $"{{ \"number\": \"{i}\" }}",
                    Number = 7,
                    Decimal = 3.14m,
                    DateTimeUtc = DateTime.UtcNow,
                    DateTimeOffset = DateTimeOffset.Now,
                    Time = TimeSpan.FromHours(1),
                    SingleSelect = "option1",
                    MultiSelect = ["option1", "option2"],
                    Color = "#00FFFF",
                    Section = new TestModel.SubModelSimple
                    {
                        Property1 = "Section",
                        Property2 = 4
                    },
                    StringList = [
                        "Item 1",
                        "Item 2"
                    ],
                    SimpleModelList =
                    [
                        new TestModel.SubModelSimple
                        {
                            Property1 = "Sub 1",
                            Property2 = 1
                        },
                        new TestModel.SubModelSimple
                        {
                            Property1 = "Sub 2",
                            Property2 = 2
                        }
                    ],
                    AdvancedModelList =
                    [
                        new TestModel.SubModelAdvanced
                        {
                            Property1 = "Sub 1",
                            Property2 = 1,
                            SubList = ["Sub-sub 1", "Sub-sub 2"]
                        }
                    ],
                    Enabled = true
                });
            }
        }

        private class DataService : IReadOnlyDataService<TestViewModel>
        {
            public bool SupportsView => throw new NotImplementedException();

            public bool SupportsSearchText => throw new NotImplementedException();

            public Task<DataResponse<TestViewModel>> GetModels(DataQuery args)
            {
                throw new NotImplementedException();
            }

            public Task<TestViewModel> GetNewListModel()
            {
                throw new NotImplementedException();
            }
        }
    }
}
