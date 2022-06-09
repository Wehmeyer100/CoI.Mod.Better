using Mafi;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;

namespace CoI.Mod.Better.Custom
{
    public interface IStorageData<Proto, State> : IIntoData<State>
        where Proto : LayoutEntityProto 
        where State : LayoutEntityBuilderState<State>
    {
        Option<Proto> Build(ProtoRegistrator registrator);

        void Load(Proto loadData);
    }
}