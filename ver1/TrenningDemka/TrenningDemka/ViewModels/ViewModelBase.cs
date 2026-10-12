using CommunityToolkit.Mvvm.ComponentModel;
using TrenningDemka.Models;

namespace TrenningDemka.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        public AppDbContext db = new AppDbContext();

        public virtual string PageTitle => string.Empty;
    }
}
