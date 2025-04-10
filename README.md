# Farm
Farm Game
# 🧱 Project Structure - Clean Architecture

## 📌 Domain Layer (Thuần C#, không phụ thuộc vào framework nào)

Chỉ chứa **logic nghiệp vụ cốt lõi & bất biến** của game.  
Ví dụ: `Inventory`, `Crop`, `Animal`, `FarmEntity`, `LandPlot`

Interfaces ở đây thường là cho các **phụ thuộc bên ngoài**, ví dụ: `IRepository`, `IClock`, `INotificationService`

- **Entities/** – Chứa các đối tượng game thuần C# (VD: `Player`, `Enemy`)
- **ValueObjects/** – Các giá trị bất biến (VD: `Vector3D`, `Health`)
- **Services/** – Xử lý logic nghiệp vụ không thuộc về một entity cụ thể (VD: `CombatService`, `InventoryService`)
- **Interfaces/** – Các interface giúp Domain Layer không phụ thuộc vào implementation (VD: `IRepository<T>`, `ILogger`)

---

## 📌 Application Layer (Xử lý luồng nghiệp vụ)

Chứa các **Use Cases**, định nghĩa rõ: người chơi **có thể làm gì**.  
Không chứa logic chi tiết, nhưng định nghĩa **luồng xử lý** và **dependencies cần thiết**.

- **UseCases/** – Chứa các use case chính của game (VD: `PlayerAttackUseCase`, `SaveGameUseCase`)
- **DTOs/** – Định nghĩa các Data Transfer Objects để truyền dữ liệu giữa các layer
- **Mediators/** *(Tuỳ chọn)* – Cơ chế trung gian giúp giảm sự phụ thuộc giữa các use cases

---

## 📌 Infrastructure

Chứa các thành phần phụ thuộc vào **Unity**, triển khai các interface từ Domain Layer.

- **Persistence/** – Chứa repository để lưu dữ liệu (`PlayerPrefsRepository`, `SQLiteRepository`)
- **UnityAdapters/** – Chứa các thành phần Unity như `MonoBehaviour`, `ScriptableObjects`
- **Logging/** – Ghi log sử dụng `UnityEngine.Debug` hoặc ghi vào file
- **Networking/** *(Nếu có online)* – Xử lý API calls, WebSockets, etc.

---

## 📌 Presentation

Giao tiếp với Application Layer qua **Controllers hoặc Adapters**.

- **UI/** – Chứa các `Prefabs`, `UIs` (`MainMenuUI`, `InventoryUI`)
- **Scenes/** – Chứa các scene của game
- **Controllers/** – Xử lý UI logic (`MainMenuController`, `InventoryController`)
- **Adapters/** *(Tuỳ chọn)* – Chuyển đổi dữ liệu giữa UI và Application Layer

---

## 📌 Framework

Chứa các tiện ích chung hỗ trợ toàn dự án.

- **DI/** – Dependency Injection container (VD: `Zenject`, `VContainer`)
- **EventBus/** – Cơ chế sự kiện (Pub-Sub model)
- **Utils/** – Các hàm tiện ích (`MathUtils`, `StringUtils`)
- **StateMachine/** *(Tuỳ chọn)* – Hệ thống FSM (Finite State Machine) nếu game có nhiều trạng thái
- **Localization/** *(Tuỳ chọn)* – Hỗ trợ đa ngôn ngữ

---

## 📌 Tests

- **UnitTests/** – Kiểm tra logic domain (`PlayerTest`, `InventoryTest`)
- **IntegrationTests/** – Kiểm tra interaction giữa các thành phần
- **PlayModeTests/** *(Tuỳ chọn)* – Kiểm thử các chức năng trong Unity

---

> Áp dụng cấu trúc Clean Architecture giúp **bảo trì dễ dàng**, **test hiệu quả**, và **tách biệt rõ ràng** giữa các phần trong dự án.
