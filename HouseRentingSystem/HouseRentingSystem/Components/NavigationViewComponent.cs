using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult<IViewComponentResult>(View());
        }
    }
}
