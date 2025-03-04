using System.ComponentModel.DataAnnotations;

namespace pv311_mvc_project.Models
{
    public interface IBaseModel<T>
    {
        T? Id { get; set; }
    }

    public class BaseModel<T> : IBaseModel<T>
    {
        [Key]
        public T? Id { get; set; }
    }
}