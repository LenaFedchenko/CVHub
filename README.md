# CVHub

![.NET](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=white&style=for-the-badge)
![C#](https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white&style=for-the-badge)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-512BD4?style=for-the-badge)
![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?logo=javascript&logoColor=black&style=for-the-badge)
![HTML5](https://img.shields.io/badge/HTML5-E34F26?logo=html5&logoColor=white&style=for-the-badge)
![CSS3](https://img.shields.io/badge/CSS3-1572B6?logo=css3&logoColor=white&style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white&style=for-the-badge)
![GitHub Actions](https://img.shields.io/badge/GitHub%20Actions-2088FF?logo=githubactions&logoColor=white&style=for-the-badge)

---

## 🇺🇦 CVHub  

### Опис  
**CVHub** — це вебзастосунок, що допомагає знаходити розробників та переглядати їхні резюме. Система дозволяє створювати акаунти, редагувати інформацію про себе, додавати навички, а також переглядати профілі інших користувачів.  

---

## 🇬🇧 CVHub  

### Description  
**CVHub** is a web application designed to help users find developers and explore their CVs. The platform allows users to create accounts, edit personal information, add skills, and browse other developers’ profiles.  

---

## 🚀 Основні можливості / Key Features
- 🔑 Реєстрація та авторизація / User registration and authentication  
- 👤 Профіль з навичками та ролями / Profile creation with skills and roles  
- 🔍 Пошук та фільтрація / Search and filtering  
- 📄 Перегляд резюме / CV viewing  
- 💾 Збереження в базі даних / Database storage  
- 🧪 Юніт-тести для надійності / Unit tests for reliability  

---

## 🖼️ Preview / Screenshots  
 
Main page
![Preview](readmePhotos/main.png)  

Account
![Profile](readmePhotos/account.png)  

---

## ⚙️ Технології / Technologies  
- **Backend:** ASP.NET Core, Entity Framework Core  
- **Frontend:** Razor Pages, HTML, CSS, JavaScript  
- **Database:** Microsoft SQL Server / SQLite  
- **Other:** Session, Logging, Unit Tests  

---

## 📂 Архітектура / Architecture  

```plaintext
CVHub
├── Controllers      # Логіка запитів / Request handling
├── Models           # Моделі даних / Data models
├── Views            # Razor Pages (UI)
├── wwwroot          # CSS, JS, static files
└── Data             # Контекст бази даних / Database context
