using Character;
using Character.View;

namespace Game.GameWin
{
    public interface IGameWinCondition
    {
        bool Validate(ICharacterView characterView);
        void Execute(ICharacterView characterView);
    }
}