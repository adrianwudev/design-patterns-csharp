
// mvvm

// model
using Hangfire.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Person
    : INotifyPropertyChanged, IDataErrorInfo
{
    public string FirstName, LastName;

    public string this[string columnName] => throw new NotImplementedException();

    public string Error => throw new NotImplementedException();

    public event PropertyChangedEventHandler? PropertyChanged;
}

// view = ui

// viewmodel
public class PersonViewModel
    : INotifyPropertyChanged
{
    private readonly Person _person;
    public PersonViewModel(Person person)
    {
        _person = person;
    }

    public string FirstName
    {
        get => _person.FirstName;
        set
        {
            if (_person.FirstName == value) return;
            _person.FirstName = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FullName));
        }
    }

    public string LastName
    {
        get => _person.LastName;
        set
        {
            if (_person.LastName == value) return;
            _person.LastName = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FullName));
        }
    }

    public string FullName
    {
        get => $"{FirstName} {LastName}".Trim();
        set
        {
            if (value == null)
            {
                FirstName = LastName = null;
                return;
            }
            var items = value.Split();
            if (items.Length > 0)
                FirstName = items[0]; 
            if (items.Length > 1)
                LastName = items[1];
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged(
        [CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this,
          new PropertyChangedEventArgs(propertyName));
    }
}
public class ViewModel
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World! View Model...");
    }
}