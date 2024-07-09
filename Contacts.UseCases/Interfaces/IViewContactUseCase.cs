namespace Contacts.UseCases.Interfaces
{
    public interface IViewContactUseCase
    {
        Task<List<CoreBusiness.Contact>> ExecuteAsync(string filterText);
    }
}