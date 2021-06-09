using SimpleSocialNetworkApp.Domain.Interfaces;

namespace SimpleSocialNetworkApp.Domain.Models
{
    public abstract class BaseEntity : IBaseEntity
    {
        public bool IsAccountActive { get; set; }
        public int Id { get; set; }
        public BaseEntity()
        {

        }
        public abstract string PrintInfo();
    }
}
