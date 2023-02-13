using DataAccess.Models;

namespace DataAccess.Data;

public interface IAddressRepository
{
    Task Create(string title, float latitude, float longitude);
    Task<AddressModel?> Get(float latitude, float longitude);
}