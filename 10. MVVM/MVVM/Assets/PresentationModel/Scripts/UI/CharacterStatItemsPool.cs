using ShootEmUp.Modules.Base;

namespace Lessons.Architecture.PM.UI
{
    public class CharacterStatItemsPool : ObjectPool<CharacterStatItemPresenter>
    {
        public CharacterStatItemsPool(ObjectFactory<CharacterStatItemPresenter> objectFactory, int initialSize) : base(objectFactory, initialSize)
        {
        }
    }
}
