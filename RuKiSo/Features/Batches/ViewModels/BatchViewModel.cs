using RuKiSo.Features.Batches.Models;
using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;

namespace RuKiSo.ViewModels
{
    public class BatchViewModel : BaseViewModel
    {
        private bool isPopupOpen;
        private int totalBatch;
        private double totalValue;
        private double projectedYield;
        public bool IsPopupOpen
        {
            get { return isPopupOpen; }
            set
            {
                isPopupOpen = value;
                OnPropertyChanged(nameof(IsPopupOpen));
            }
        }
        public int TotalBatch
        {
            get { return totalBatch; }
            set
            {
                totalBatch = value;
                OnPropertyChanged(nameof(TotalBatch));
            }
        }

        public double TotalValue
        {
            get { return totalValue; }
            set
            {
                totalValue = value;
                OnPropertyChanged(nameof(TotalValue));
            }
        }

        public double ProjectedYield
        {
            get { return projectedYield; }
            set
            {
                projectedYield = value;
                OnPropertyChanged(nameof(ProjectedYield));
            }
        }
        public ObservableCollection<BatchIngredientDTO> Ingredients { get; set; }
        public ObservableCollection<BatchDTO> Batches { get; set; }
        public BatchViewModel()
        {
            InitData();
        }

        private void InitData()
        {
            Ingredients = new()
            {
                new() {Id = 1, IngredientName = "Men lá", StoredQuantity = 10, PricePerUnit = 200},
                new() {Id = 2, IngredientName = "Men thuốc bắc", StoredQuantity = 20, PricePerUnit = 150},
                new() {Id = 3, IngredientName = "Nếp cái hoa vàng", StoredQuantity = 500, PricePerUnit = 20},
                new() {Id = 4, IngredientName = "Nếp đen", StoredQuantity = 400, PricePerUnit = 18},
                new() {Id = 5, IngredientName = "Đòng đòng", StoredQuantity = 30, PricePerUnit = 50},
            };
            Batches = new()
            {
                new BatchDTO
                {
                    Id = 1,
                    Name = "Rượu trắng 45",
                    StartDate = DateTime.Now,
                    EstimateEndDate = DateTime.Now.AddMonths(3),
                    Ingredients = new List<BatchIngredientDTO>
                    {
                        new BatchIngredientDTO { Id = 1, IngredientName = "Men lá", StoredQuantity = 10, UsedQuantity = 2, PricePerUnit = 200 },
                        new BatchIngredientDTO { Id = 3, IngredientName = "Nếp cái hoa vàng", StoredQuantity = 500, UsedQuantity = 100, PricePerUnit = 20 },
                    }
                    },
                new BatchDTO
                {
                    Id = 2,
                    Name = "Rượu thuốc Bắc",
                    StartDate = DateTime.Now,
                    EstimateEndDate = DateTime.Now.AddMonths(4),
                    Ingredients = new List<BatchIngredientDTO>
                    {
                        new BatchIngredientDTO { Id = 2, IngredientName = "Men thuốc bắc", StoredQuantity = 20, UsedQuantity = 3, PricePerUnit = 150 },
                        new BatchIngredientDTO { Id = 4, IngredientName = "Nếp đen", StoredQuantity = 400, UsedQuantity = 150, PricePerUnit = 18 },
                    }
                },
                new BatchDTO
                {
                    Id = 3,
                    Name = "Rượu trắng 40",
                    StartDate = DateTime.Now,
                    EstimateEndDate = DateTime.Now.AddMonths(5),
                    Ingredients = new List<BatchIngredientDTO>
                    {
                        new BatchIngredientDTO { Id = 5, IngredientName = "Đòng đòng", StoredQuantity = 30, UsedQuantity = 5, PricePerUnit = 50 },
                        new BatchIngredientDTO { Id = 1, IngredientName = "Men lá", StoredQuantity = 10, UsedQuantity = 3, PricePerUnit = 200 },
                    }
                },
                new BatchDTO
                {
                    Id = 4,
                    Name = "Rượu đòng đòng 45",
                    StartDate = DateTime.Now,
                    EstimateEndDate = DateTime.Now.AddMonths(5),
                    Ingredients = new List<BatchIngredientDTO>
                    {
                        new BatchIngredientDTO { Id = 5, IngredientName = "Đòng đòng", StoredQuantity = 30, UsedQuantity = 5, PricePerUnit = 50 },
                        new BatchIngredientDTO { Id = 1, IngredientName = "Men lá", StoredQuantity = 10, UsedQuantity = 3, PricePerUnit = 200 },
                    }
                },
                new BatchDTO
                {
                    Id = 5,
                    Name = "Rượu đòng đòng 40",
                    StartDate = DateTime.Now,
                    EstimateEndDate = DateTime.Now.AddMonths(5),
                    Ingredients = new List<BatchIngredientDTO>
                    {
                        new BatchIngredientDTO { Id = 5, IngredientName = "Đòng đòng", StoredQuantity = 30, UsedQuantity = 5, PricePerUnit = 50 },
                        new BatchIngredientDTO { Id = 1, IngredientName = "Men lá", StoredQuantity = 10, UsedQuantity = 3, PricePerUnit = 200 },
                    }
                },
                new BatchDTO
                {
                    Id = 6,
                    Name = "Rượu đòng đòng 40",
                    StartDate = DateTime.Now,
                    EstimateEndDate = DateTime.Now.AddMonths(5),
                    Ingredients = new List<BatchIngredientDTO>
                    {
                        new BatchIngredientDTO { Id = 5, IngredientName = "Đòng đòng", StoredQuantity = 30, UsedQuantity = 5, PricePerUnit = 50 },
                        new BatchIngredientDTO { Id = 1, IngredientName = "Men lá", StoredQuantity = 10, UsedQuantity = 3, PricePerUnit = 200 },
                    }
                },
                 new BatchDTO
                 {
                     Id = 7,
                     Name = "Rượu đòng đòng 40",
                     StartDate = DateTime.Now,
                     EstimateEndDate = DateTime.Now.AddMonths(5),
                     Ingredients = new List<BatchIngredientDTO>
                     {
                         new BatchIngredientDTO { Id = 5, IngredientName = "Đòng đòng", StoredQuantity = 30, UsedQuantity = 5, PricePerUnit = 50 },
                         new BatchIngredientDTO { Id = 1, IngredientName = "Men lá", StoredQuantity = 10, UsedQuantity = 3, PricePerUnit = 200 },
                     }
                 }
            };
            UpdateCardsData();
        }

        private void UpdateCardsData()
        {
            TotalBatch = Batches.Count;
            TotalValue = Batches.Sum(batch => batch.Value);
            ProjectedYield = 100;
        }
    }
}
