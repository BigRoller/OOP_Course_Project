using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using System.Windows;

namespace FashionHub.Models
{
  public class ClothingItem : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;


    private ObservableCollection<string> _imagePaths = new ObservableCollection<string>();

    private const string DefaultImagePath = "C:\\Users\\glora\\Desktop\\ООП курсовой\\FashionHub\\FashionHub\\Resources\\icons\\no-image.png";

    [NotMapped]
    public ObservableCollection<string> ImagePaths
    {
      get
      {
        if (_imagePaths.Count == 0)
        {
          _imagePaths.Add(DefaultImagePath);
        }
        else if (_imagePaths.Count > 1 && _imagePaths.Contains(DefaultImagePath))
        {
          _imagePaths.Remove(DefaultImagePath);
        }

        return _imagePaths;
      }
      set
      {
        _imagePaths = value;
        OnPropertyChanged(nameof(ImagePaths));
      }
    }


    [Key]
    public int ProductId { get; set; }

    private string shortName;
    [Required(ErrorMessage = "This field is required.")]
    [StringLength(50, ErrorMessage = "Short Name must be less than 50 characters.")]
    public string ShortName
    {
      get => shortName;
      set
      {
        shortName = value;
        OnPropertyChanged(nameof(ShortName));
      }
    }

    private string fullName;
    [Required(ErrorMessage = "This field is required.")]
    [StringLength(100, ErrorMessage = "Full Name must be less than 100 characters.")]
    public string FullName
    {
      get => fullName;
      set
      {
        fullName = value;
        OnPropertyChanged(nameof(FullName));
      }
    }

    private string description;
    [Required(ErrorMessage = "This field is required.")]
    [StringLength(500, ErrorMessage = "Description must be less than 500 characters.")]
    public string Description
    {
      get => description;
      set
      {
        description = value;
        OnPropertyChanged(nameof(Description));
      }
    }

    public string Images
    {
      get => string.Join(";", ImagePaths);
      set
      { 
        ImagePaths = new ObservableCollection<string>(string.IsNullOrWhiteSpace(value)
          ? Enumerable.Empty<string>()
          : value.Split(';'));
        OnPropertyChanged(nameof(Images));
      }
    }

    private string category;
    [Required(ErrorMessage = "This field is required.")]
    [StringLength(30, ErrorMessage = "Category must be less than 30 characters.")]
    public string Category
    {
      get => category;
      set
      {
        category = value;
        OnPropertyChanged(nameof(Category));
      }
    }

    private string color;
    [Required(ErrorMessage = "This field is required.")]
    [RegularExpression(@"^(\s*[a-z]{1,20}\s*,?)+$", ErrorMessage = "Colors must be separated by commas and use uppercase letters (e.g., green, blue, yellow).")]
    public string Color
    {
      get => color;
      set
      {
        color = value;
        OnPropertyChanged(nameof(Color));
      }
    }

    private string size;
    [Required(ErrorMessage = "This field is required.")]
    [RegularExpression(@"^(\s*[A-Z]{1,3}\s*,?)+$", ErrorMessage = "Sizes must be separated by commas and use uppercase letters (e.g., L, M, XXL).")]
    public string Size
    {
      get => size;
      set
      {
        size = value;
        OnPropertyChanged(nameof(Size));
      }
    }

    private string deliveryCountry;
    [Required(ErrorMessage = "This field is required.")]
    [StringLength(50, ErrorMessage = "Country of delivery  must be less than 50 characters.")]
    public string DeliveryCountry
    {
      get => deliveryCountry;
      set
      {
        deliveryCountry = value;
        OnPropertyChanged(nameof(DeliveryCountry));
      }
    }

    private decimal price;
    [Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10,000.")]
    public decimal Price
    {
      get => price;
      set
      {
        price = value;
        OnPropertyChanged(nameof(Price));
      }
    }

    private string compound;
    [Required(ErrorMessage = "This field is required.")]
    [StringLength(50, ErrorMessage = "Compound must be less than 50 characters.")]
    public string Compound
    {
      get => compound;
      set
      {
        compound = value;
        OnPropertyChanged(nameof(Compound));
      }
    }

    private int salesCount;
    [Required(ErrorMessage = "This field is required.")]
    [Range(0, 10000, ErrorMessage = "Sales сount must be between 0 and 10 000")]
    public int SalesCount
    {
      get => salesCount;
      set
      {
        salesCount = value;
        OnPropertyChanged(nameof(SalesCount));
      }
    }

    private int quantity;
    [Required(ErrorMessage = "This field is required.")]
    [Range(0, 1000, ErrorMessage = "Quantity must be between 0 and 1 000")]
    public int Quantity
    {
      get => quantity;
      set
      {
        quantity = value;
        OnPropertyChanged(nameof(Quantity));
      }
    }

    [NotMapped]
    public float Rating
    {
      get
      {
        if (Comments == null || Comments.Count == 0)
          return 0;

        return (float)Math.Round(Comments.Average(c => c.Rate), 2);
      }
    }

    public List<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    protected void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public override string ToString()
    {
      return ShortName;
    }

  }



  public class ClothingViewModel
  {
    private readonly IUnitOfWork _unitOfWork;

    public ClothingViewModel(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public void SaveChanges()
    {
      try
      {
        _unitOfWork.Commit(); // Сохраняем данные
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Ошибка сохранения: {ex.Message}");
        _unitOfWork.Rollback(); // Отменяем изменения
      }
    }
  }

}
