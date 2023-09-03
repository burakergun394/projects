using Domain.Contexts;

namespace Application.Features.Contexts;

public class ContextService : IContextService
{
    private readonly static AsyncLocal<Context> _asyncLocal = new();

    public Context GetContext() => _asyncLocal.Value;

    public void SetContext(Context context) => _asyncLocal.Value = context;
}
