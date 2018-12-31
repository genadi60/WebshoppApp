using System;

namespace WebshopApp.Models.Base
{
    [Serializable]
    public class BaseModel<T>
    {
        public T Id { get; set; }
    }
}
