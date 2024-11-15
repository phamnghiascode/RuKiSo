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
        private ProductDTO selectedProduct;
        private bool isEditCookPopupOpen;
        private int totalBatch;
        private double totalValue;
        private double projectedYield;
        private DateTime startDate;
        private DateTime estimateEndDate;
        public ICommand AddBatchCommand { get; }
        public ICommand EditCookBatchCommand { get; }
        public ICommand DeleteBatchCommand { get; }
        public ICommand SaveBatchCommand { get; }

        public BatchDTO SelectedBatch
        {
            get { return selectedBatch; }
            set
            {
                selectedBatch = value;
                OnPropertyChanged(nameof(SelectedBatch));
            }
        }
        public ProductDTO SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
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

        public bool IsEditCookPopupOpen
        {
            get { return isEditCookPopupOpen; }
            set
            {
                isEditCookPopupOpen = value;
                OnPropertyChanged(nameof(IsEditCookPopupOpen));
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
        public ObservableCollection<BatchDTO> Batches { get; set; } = new ObservableCollection<BatchDTO>();
        public ObservableCollection<BatchDTO> AllBatches { get; set; }
        public ObservableCollection<ProductDTO> Products { get; set; }
       
        public BatchViewModel()
        {
            EditCookBatchCommand = new RelayCommand<BatchDTO>(EditCookBatch);
            SaveBatchCommand = new RelayCommand(SaveBatch);
            DeleteBatchCommand = new RelayCommand<BatchDTO>(DeleteBatch);
            AddBatchCommand = new RelayCommand(AddBatch);
            InitData();
        }

        private void SaveBatch()
        {
            var updateBatch = Batches.FirstOrDefault(p => p.Id == SelectedBatch.Id);
            if (updateBatch != null) 
            {
                updateBatch.Product = SelectedBatch.Product;
                updateBatch.StartDate = SelectedBatch.StartDate;
                updateBatch.EstimateEndDate = SelectedBatch.EstimateEndDate;
                updateBatch.Ingredients = SelectedBatch.Ingredients;
                updateBatch.Yield = SelectedBatch.Yield;
                UpdateBatches();
                IsEditCookPopupOpen = false;
            }
        }

        private void EditCookBatch(BatchDTO batch)
        {
            SelectedBatch = batch;  
            IsEditCookPopupOpen = true;
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
                Product = SelectedProduct,
                StartDate = StartDate,
                EstimateEndDate = EstimateEndDate,
                Ingredients = selectedIngredients
            };

            Batches.Add(newBatch);
            UpdateCardsData();
        }

        private void InitData()
        {
            Ingredients = new ObservableCollection<BatchIngredientDTO>
            {
                new() {Id = 1, IngredientName = "Men lá", StoredQuantity = 10, PricePerUnit = 200},
                new() {Id = 2, IngredientName = "Men thuốc bắc", StoredQuantity = 20, PricePerUnit = 150},
                new() {Id = 3, IngredientName = "Nếp cái hoa vàng", StoredQuantity = 500, PricePerUnit = 20},
                new() {Id = 4, IngredientName = "Nếp đen", StoredQuantity = 400, PricePerUnit = 18},
                new() {Id = 5, IngredientName = "Đòng đòng", StoredQuantity = 30, PricePerUnit = 50},
            };
            Products = new ObservableCollection<ProductDTO>
            {
                new() {Id = 1, Name = "Rượu trắng thượng hạng", Ingredients = "Nếp cái hoa vàng, men thuốc bắc", Quantity = 300, Price = 50000},
                new() {Id = 2, Name = "Rượu trắng", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 700, Price = 45000},
                new() {Id = 3, Name = "Rượu trắng nhẹ", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 100, Price = 40000},
                new() {Id = 4, Name = "Rượu đòng đòng", Ingredients = "Nếp cái hoa vàng, bông lúa non, men thuốc bắc", Quantity = 500, Price = 75000},
                new() {Id = 5, Name = "Rượu bách nhật", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 5, Price = 40000},
            };
            AllBatches = new()
            {
                new BatchDTO
                {
                    Id = 1,
                    Product = Products[0],
                    StartDate = DateTime.Now,
                    EstimateEndDate = DateTime.Now.AddMonths(3),
                    Ingredients = new List<BatchIngredientDTO>
                    {
                        new BatchIngredientDTO { Id = 1, IngredientName = "Men lá", StoredQuantity = 10, UsedQuantity = 2, PricePerUnit = 200, IsSelected = true },
                        new BatchIngredientDTO { Id = 3, IngredientName = "Nếp cái hoa vàng", StoredQuantity = 500, UsedQuantity = 100, PricePerUnit = 20, IsSelected = true },
                    }
                    },
                new BatchDTO
                {
                    Id = 2,
                    Product = Products[1],
                    StartDate = DateTime.Now,
                    EstimateEndDate = DateTime.Now.AddMonths(4),
                    Ingredients = new List<BatchIngredientDTO>
                    {
                        new BatchIngredientDTO { Id = 2, IngredientName = "Men thuốc bắc", StoredQuantity = 20, UsedQuantity = 3, PricePerUnit = 150 , IsSelected = true},
                        new BatchIngredientDTO { Id = 4, IngredientName = "Nếp đen", StoredQuantity = 400, UsedQuantity = 150, PricePerUnit = 18, IsSelected = true },
                    }
                },
                new BatchDTO
                {
                    Id = 3,
                    Product = Products[2],
                    StartDate = DateTime.Now,
                    EstimateEndDate = DateTime.Now.AddMonths(5),
                    Ingredients = new List<BatchIngredientDTO>
                    {
                        new BatchIngredientDTO { Id = 5, IngredientName = "Đòng đòng", StoredQuantity = 30, UsedQuantity = 5, PricePerUnit = 50, IsSelected = true },
                        new BatchIngredientDTO { Id = 1, IngredientName = "Men lá", StoredQuantity = 10, UsedQuantity = 3, PricePerUnit = 200, IsSelected = true },
                    }
                },
                new BatchDTO
                {
                    Id = 4,
                    Product = Products[3],
                    StartDate = DateTime.Now,
                    EstimateEndDate = DateTime.Now.AddMonths(5),
                    Ingredients = new List<BatchIngredientDTO>
                    {
                        new BatchIngredientDTO { Id = 5, IngredientName = "Đòng đòng", StoredQuantity = 30, UsedQuantity = 5, PricePerUnit = 50, IsSelected = true },
                        new BatchIngredientDTO { Id = 1, IngredientName = "Men lá", StoredQuantity = 10, UsedQuantity = 3, PricePerUnit = 200, IsSelected = true },
                    }
                },
                new BatchDTO
                {
                    Id = 5,
                    Product = Products[4],
                    StartDate = DateTime.Now,
                    EstimateEndDate = DateTime.Now.AddMonths(5),
                    Ingredients = new List<BatchIngredientDTO>
                    {
                        new BatchIngredientDTO { Id = 5, IngredientName = "Đòng đòng", StoredQuantity = 30, UsedQuantity = 5, PricePerUnit = 50 , IsSelected = true},
                        new BatchIngredientDTO { Id = 1, IngredientName = "Men lá", StoredQuantity = 10, UsedQuantity = 3, PricePerUnit = 200 , IsSelected = true},
                    }
                },
                new BatchDTO
                {
                    Id = 6,
                    Product = Products[0],
                    StartDate = DateTime.Now,
                    EstimateEndDate = DateTime.Now.AddMonths(5),
                    Ingredients = new List<BatchIngredientDTO>
                    {
                        new BatchIngredientDTO { Id = 5, IngredientName = "Đòng đòng", StoredQuantity = 30, UsedQuantity = 5, PricePerUnit = 50 , IsSelected = true},
                        new BatchIngredientDTO { Id = 1, IngredientName = "Men lá", StoredQuantity = 10, UsedQuantity = 3, PricePerUnit = 200 , IsSelected = true},
                    }
                },
                 new BatchDTO
                 {
                     Id = 7,
                     Product = Products[1],
                     StartDate = DateTime.Now,
                     EstimateEndDate = DateTime.Now.AddMonths(5),
                     Ingredients = new List<BatchIngredientDTO>
                     {
                         new BatchIngredientDTO { Id = 5, IngredientName = "Đòng đòng", StoredQuantity = 30, UsedQuantity = 5, PricePerUnit = 50 , IsSelected = true},
                         new BatchIngredientDTO { Id = 1, IngredientName = "Men lá", StoredQuantity = 10, UsedQuantity = 3, PricePerUnit = 200 , IsSelected = true},
                     }
                 }
            };
            UpdateBatches();
        }
        private void UpdateBatches()
        {
            Batches.Clear();
            foreach (var batch in AllBatches.Where(b => b.Yield == 0))
            {
                Batches.Add(batch);
            }
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
