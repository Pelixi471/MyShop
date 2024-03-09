using Domain.Entites;

namespace FrontendBlazor.Pages
{
    public partial class ProductCatalogPage
    {

        private IReadOnlyList<Product> _products;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _products = ProductCatalog.GetProducts();
        }
        private void GoToPage(int id)
        {
            string url = "/Product/" + id;
            NavigationManager.NavigateTo(url);
        }

        private void NavigateToAddPage()
        {
            NavigationManager.NavigateTo("/add");
        }
    }
}

