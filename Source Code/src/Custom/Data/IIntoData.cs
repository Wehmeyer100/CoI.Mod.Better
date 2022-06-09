using Mafi;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;

namespace CoI.Mod.Better.Custom
{
    public interface IIntoData<State> where State : LayoutEntityBuilderState<State>
    {
        Option<State> Into(ProtoRegistrator registrator);
    }
}