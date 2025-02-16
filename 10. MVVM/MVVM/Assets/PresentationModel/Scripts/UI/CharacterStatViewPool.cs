using Sekibura.Modules.Base;

namespace OTUSHW.MVVM.UI.View
{
    public sealed class CharacterStatViewPool : ObjectPool<CharacterStatView>
    {
        public CharacterStatViewPool(ObjectFactory<CharacterStatView> objectFactory, int initialSize) : base(objectFactory, initialSize)
        {
        }
    }
}
