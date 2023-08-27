namespace Domain.Contexts;

public interface IContextService
{
    Context GetContext();
    void SetContext(Context context);
}