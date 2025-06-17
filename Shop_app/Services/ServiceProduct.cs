using Microsoft.EntityFrameworkCore;
using Shop_app.Models;

namespace Shop_app.Services
{
    public interface IServiceProducts
    {
        Task<Product?> CreateAsync(Product? product); // Асинхронний метод створення продукту
        Task<IEnumerable<Product>> ReadAsync(); // Асинхронний метод отримання всіх продуктів
        Task<Product?> GetByIdAsync(int id); // Асинхронний метод отримання продукту з його ID
        Task<Product?> UpdateAsync(int id, Product? product); // Асинхронний метод оновлення продукту
        Task<bool> DeleteAsync(int id); // Асинхронний метод видалення продукту по ID
    }

    public class ServiceProducts : IServiceProducts
    {
        private readonly ShopContext _shopContext; // Хранит контекст базы данных для работы с продуктами
        private readonly ILogger<ServiceProducts> _logger; // Логгер для записи событий и ошибок

        // Конструктор класса, который принимает контекст и логгер через внедрение зависимостей
        public ServiceProducts(ShopContext productContext, ILogger<ServiceProducts> logger)
        {
            _shopContext = productContext; // Инициализация контекста базы данных
            _logger = logger; // Инициализация логгера
        }

        // Метод для создания нового продукта
        public async Task<Product?> CreateAsync(Product? product)
        {
            // Проверка, является ли продукт нулевым
            if (product == null)
            {
                _logger.LogWarning("Попытка создать продукт с нулевым значением."); // Логирование предупреждения
                return null; // Возврат нуля, если продукт нулевой
            }

            // Добавление продукта в контекст базы данных
            await _shopContext.Products.AddAsync(product);
            // Сохранение изменений в базе данных
            await _shopContext.SaveChangesAsync();
            return product; // Возврат созданного продукта
        }

        // Метод для удаления продукта по его ID
        public async Task<bool> DeleteAsync(int id)
        {
            // Поиск продукта в базе данных по его ID
            var product = await _shopContext.Products.FindAsync(id);
            // Если продукт не найден, вернуть false
            if (product == null)
            {
                return false;
            }

            // Удаление продукта из контекста базы данных
            _shopContext.Products.Remove(product);
            // Сохранение изменений в базе данных
            await _shopContext.SaveChangesAsync();
            return true; // Возврат true, если продукт успешно удален
        }

        // Метод для получения продукта по его ID
        public async Task<Product?> GetByIdAsync(int id)
        {
            // Поиск продукта в базе данных по его ID
            return await _shopContext.Products.FindAsync(id);
        }

        // Метод для получения всех продуктов из базы данных
        public async Task<IEnumerable<Product>> ReadAsync()
        {
            // Возврат списка всех продуктов
            return await _shopContext.Products.ToListAsync();
        }

        // Метод для обновления существующего продукта
        public async Task<Product?> UpdateAsync(int id, Product? product)
        {
            // Проверка, является ли продукт нулевым или идентификатор не совпадает
            if (product == null || id != product.Id)
            {
                _logger.LogWarning($"Несоответствие идентификатора продукта. Ожидался {id}, получен {product?.Id}."); // Логирование предупреждения
                return null; // Возврат нуля, если есть несоответствие
            }

            try
            {
                // Обновление продукта в контексте базы данных
                _shopContext.Products.Update(product);
                // Сохранение изменений в базе данных
                await _shopContext.SaveChangesAsync();
                return product; // Возврат обновленного продукта
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Логирование ошибки при обновлении продукта
                _logger.LogError(ex, "Ошибка при обновлении продукта с идентификатором {Id}.", id);
                return null; // Возврат нуля в случае ошибки
            }
        }

    }
}
