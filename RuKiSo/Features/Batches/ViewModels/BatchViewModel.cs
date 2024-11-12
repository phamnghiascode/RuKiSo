using CommunityToolkit.Mvvm.Input;
using RuKiSo.Features.Batches.Models;
using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RuKiSo.ViewModels
{
    public partial class BatchViewModel : BaseViewModel
    {
        private BatchDTO selectedBatch;
        private bool isCookPopupOpen;
        private bool isEditPopupOpen;
        private int totalBatch;
        private double totalValue;
        private double projectedYield;
        private string batchName;
        private DateTime startDate;
        private DateTime estimateEndDate;
        public ICommand AddBatchCommand { get; }
        public ICommand EditBatchCommand { get; }
        public ICommand CookBatchCommand { get; }
        public ICommand DeleteBatchCommand { get; }

        public BatchDTO SelectedBatch
        {
            get { return selectedBatch; }
            set
            {
                selectedBatch = value;
                OnPropertyChanged(nameof(SelectedBatch));
            }
        }

        public string BatchName
        {
            get { return batchName; }
            set
            {
                batchName = value;
                OnPropertyChanged(nameof(BatchName));
            }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public DateTime EstimateEndDate
        {
            get { return estimateEndDate; }
            set
            {
                estimateEndDate = value;
                OnPropertyChanged(nameof(EstimateEndDate));
            }
        }
        public bool IsCookPopupOpen
        {
            get { return isCookPopupOpen; }
            set
            {
                isCookPopupOpen = value;
                OnPropertyChanged(nameof(IsCookPopupOpen));
            }
        }

        public bool IsEditPopupOpen
        {
            get { return isEditPopupOpen; }
            set
            {
                isEditPopupOpen = value;
                OnPropertyChanged(nameof(IsEditPopupOpen));
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
            EditBatchCommand = new RelayCommand<BatchDTO>(EditBatch);
            CookBatchCommand = new RelayCommand<BatchDTO>(CookBatch);
            DeleteBatchCommand = new RelayCommand<BatchDTO>(DeleteBatch);
            AddBatchCommand = new RelayCommand(AddBatch);
            InitData();
        }

        private void CookBatch(BatchDTO batch)
        {
            IsEditPopupOpen = true;
        }

        private void EditBatch(BatchDTO batch)
        {
            IsEditPopupOpen = true;
        }

        private void DeleteBatch(BatchDTO batch)
        {
            if (batch != null && Batches.Contains(batch))
            {
                Batches.Remove(batch);
                UpdateCardsData();
            }
        }

        public List<BatchIngredientDTO> GetSelectedIngredients()
        {
            return Ingredients.Where(ingredient => ingredient.IsSelected && ingredient.UsedQuantity > 0).ToList();
        }

        private void AddBatch()
        {
            var selectedIngredients = GetSelectedIngredients();

            var newBatch = new BatchDTO
            {
                Name = BatchName,
                StartDate = StartDate,
                EstimateEndDate = EstimateEndDate,
                Ingredients = selectedIngredients
            };

            Batches.Add(newBatch);
            UpdateCardsData();
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
