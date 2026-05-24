using GNEZDO.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GNEZDO.Data;

/// <summary>
/// Контекст базы данных платформы «ГНЕЗДО».
/// Предоставляет доступ к сущностям: пользователи, статьи, форум, специалисты, консультации.
/// Наследуется от IdentityDbContext&lt;User&gt; для поддержки аутентификации ASP.NET Core Identity.
/// </summary>
public class GnezdoContext : IdentityDbContext<User>
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="GnezdoContext"/>.
    /// </summary>
    /// <param name="options">Настройки подключения к базе данных.</param>
    public GnezdoContext(DbContextOptions<GnezdoContext> options)
        : base(options)
    {
    }

    #region DbSets — Таблицы базы данных

    /// <summary>
    /// Статьи платформы (публикации о психологии, развитии детей и т.д.)
    /// </summary>
    public DbSet<Article> Articles { get; set; } = null!;

    /// <summary>
    /// Категории статей для группировки контента
    /// </summary>
    public DbSet<Category> Categories { get; set; } = null!;

    /// <summary>
    /// Темы обсуждений на форуме поддержки
    /// </summary>
    public DbSet<ForumTopic> ForumTopics { get; set; } = null!;

    /// <summary>
    /// Сообщения пользователей в рамках тем форума
    /// </summary>
    public DbSet<ForumPost> ForumPosts { get; set; } = null!;

    /// <summary>
    /// Профили специалистов (психологи, педагоги, консультанты)
    /// </summary>
    public DbSet<Specialist> Specialists { get; set; } = null!;

    /// <summary>
    /// Записи пользователей на консультации к специалистам
    /// </summary>
    public DbSet<Consultation> Consultations { get; set; } = null!;

    #endregion

    /// <summary>
    /// Конфигурирует модели данных при создании схемы базы данных.
    /// Использует Fluent API для настройки ключей, индексов, ограничений и связей.
    /// </summary>
    /// <param name="modelBuilder">Конструктор модели EF Core.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Применяем конфигурации всех сущностей
        ConfigureCategory(modelBuilder);
        ConfigureArticle(modelBuilder);
        ConfigureForumTopic(modelBuilder);
        ConfigureForumPost(modelBuilder);
        ConfigureSpecialist(modelBuilder);
        ConfigureConsultation(modelBuilder);

        // Заполняем базу начальными данными (seed)
        SeedData(modelBuilder);
    }

    #region Конфигурация сущностей (Fluent API)

    private static void ConfigureCategory(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("Name");

            entity.Property(e => e.Description)
                  .HasMaxLength(500)
                  .HasColumnName("Description");

            entity.Property(e => e.Slug)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("Slug");

            // Уникальный индекс для SEO-friendly URL
            entity.HasIndex(e => e.Slug).IsUnique().HasDatabaseName("IX_Categories_Slug");
        });
    }

    private static void ConfigureArticle(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Title)
                  .IsRequired()
                  .HasMaxLength(200)
                  .HasColumnName("Title");

            entity.Property(e => e.Summary)
                  .IsRequired()
                  .HasMaxLength(500)
                  .HasColumnName("Summary");

            entity.Property(e => e.Content)
                  .IsRequired()
                  .HasColumnType("TEXT")
                  .HasColumnName("Content");

            entity.Property(e => e.Slug)
                  .IsRequired()
                  .HasMaxLength(200)
                  .HasColumnName("Slug");

            entity.Property(e => e.ImageUrl)
                  .HasMaxLength(500)
                  .HasColumnName("ImageUrl");

            entity.Property(e => e.Author)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("Author");

            entity.Property(e => e.PublishedAt)
                  .IsRequired()
                  .HasColumnName("PublishedAt");

            entity.Property(e => e.CreatedAt)
                  .IsRequired()
                  .HasDefaultValueSql("CURRENT_TIMESTAMP")
                  .HasColumnName("CreatedAt");

            entity.Property(e => e.UpdatedAt)
                  .HasColumnName("UpdatedAt");

            entity.Property(e => e.IsPublished)
                  .IsRequired()
                  .HasDefaultValue(false)
                  .HasColumnName("IsPublished");

            // Уникальный slug для ЧПУ
            entity.HasIndex(e => e.Slug).IsUnique().HasDatabaseName("IX_Articles_Slug");

            // Индекс для быстрого поиска опубликованных статей
            entity.HasIndex(e => new { e.IsPublished, e.PublishedAt })
                  .HasDatabaseName("IX_Articles_Published");

            // Связь с категорией: статья не удаляется при удалении категории
            entity.HasOne(e => e.Category)
                  .WithMany(c => c.Articles)
                  .HasForeignKey(e => e.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Articles_Categories");
        });
    }

    private static void ConfigureForumTopic(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ForumTopic>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Title)
                  .IsRequired()
                  .HasMaxLength(200)
                  .HasColumnName("Title");

            entity.Property(e => e.Description)
                  .HasMaxLength(500)
                  .HasColumnName("Description");

            entity.Property(e => e.CreatedAt)
                  .IsRequired()
                  .HasDefaultValueSql("CURRENT_TIMESTAMP")
                  .HasColumnName("CreatedAt");

            entity.Property(e => e.PostCount)
                  .IsRequired()
                  .HasDefaultValue(0)
                  .HasColumnName("PostCount");

            // Индекс для сортировки по дате создания
            entity.HasIndex(e => e.CreatedAt).HasDatabaseName("IX_ForumTopics_CreatedAt");
        });
    }

    private static void ConfigureForumPost(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ForumPost>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Content)
                  .IsRequired()
                  .HasColumnType("TEXT")
                  .HasColumnName("Content");

            entity.Property(e => e.CreatedAt)
                  .IsRequired()
                  .HasDefaultValueSql("CURRENT_TIMESTAMP")
                  .HasColumnName("CreatedAt");

            // Связь с пользователем: пост не удаляется при удалении пользователя
            entity.HasOne(e => e.User)
                  .WithMany(u => u.ForumPosts)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_ForumPosts_Users");

            // Связь с темой: посты удаляются при удалении темы
            entity.HasOne(e => e.ForumTopic)
                  .WithMany(t => t.Posts)
                  .HasForeignKey(e => e.ForumTopicId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK_ForumPosts_ForumTopics");

            // Индекс для быстрого получения постов по теме
            entity.HasIndex(e => e.ForumTopicId).HasDatabaseName("IX_ForumPosts_TopicId");
        });
    }

    private static void ConfigureSpecialist(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Specialist>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.FirstName)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("FirstName");

            entity.Property(e => e.LastName)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("LastName");

            entity.Property(e => e.Specialization)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("Specialization");

            entity.Property(e => e.Description)
                  .HasMaxLength(1000)
                  .HasColumnName("Description");

            entity.Property(e => e.ExperienceYears)
                  .IsRequired()
                  .HasColumnName("ExperienceYears");

            entity.Property(e => e.PricePerHour)
                  .IsRequired()
                  .HasColumnType("decimal(10,2)")
                  .HasColumnName("PricePerHour");

            entity.Property(e => e.PhotoUrl)
                  .HasMaxLength(500)
                  .HasColumnName("PhotoUrl");

            entity.Property(e => e.IsAvailable)
                  .IsRequired()
                  .HasDefaultValue(true)
                  .HasColumnName("IsAvailable");

            // Индекс для поиска по специализации
            entity.HasIndex(e => e.Specialization).HasDatabaseName("IX_Specialists_Specialization");

            // Индекс для фильтрации доступных специалистов
            entity.HasIndex(e => e.IsAvailable).HasDatabaseName("IX_Specialists_Available");
        });
    }

    private static void ConfigureConsultation(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Consultation>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.DateTime)
                  .IsRequired()
                  .HasColumnName("DateTime");

            entity.Property(e => e.DurationMinutes)
                  .IsRequired()
                  .HasColumnName("DurationMinutes");

            entity.Property(e => e.Status)
                  .IsRequired()
                  .HasColumnName("Status");

            entity.Property(e => e.ClientComment)
                  .HasMaxLength(1000)
                  .HasColumnName("ClientComment");

            entity.Property(e => e.SpecialistComment)
                  .HasMaxLength(1000)
                  .HasColumnName("SpecialistComment");

            entity.Property(e => e.CreatedAt)
                  .IsRequired()
                  .HasDefaultValueSql("CURRENT_TIMESTAMP")
                  .HasColumnName("CreatedAt");

            // Связь с пользователем
            entity.HasOne(e => e.User)
                  .WithMany(u => u.Consultations)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Consultations_Users");

            // Связь со специалистом
            entity.HasOne(e => e.Specialist)
                  .WithMany(s => s.Consultations)
                  .HasForeignKey(e => e.SpecialistId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Consultations_Specialists");

            // Индекс для поиска записей по дате
            entity.HasIndex(e => e.DateTime).HasDatabaseName("IX_Consultations_DateTime");

            // Составной индекс для фильтрации по статусу и дате
            entity.HasIndex(e => new { e.Status, e.DateTime })
                  .HasDatabaseName("IX_Consultations_StatusDate");
        });
    }

    #endregion

    #region Seed Data — Начальное наполнение базы

    /// <summary>
    /// Заполняет базу данных тестовыми данными при первой миграции.
    /// Включает категории, статьи, специалистов и темы форума.
    /// </summary>
    private static void SeedData(ModelBuilder modelBuilder)
    {
        SeedCategories(modelBuilder);
        SeedArticles(modelBuilder);
        SeedSpecialists(modelBuilder);
        SeedForumTopics(modelBuilder);
    }

    private static void SeedCategories(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Психология",
                Description = "Психологическая поддержка родителей: стресс, выгорание, эмоции",
                Slug = "psychology"
            },
            new Category
            {
                Id = 2,
                Name = "Развитие детей",
                Description = "Всё о развитии ребёнка: речь, моторика, мышление, социализация",
                Slug = "child-development"
            },
            new Category
            {
                Id = 3,
                Name = "Здоровье",
                Description = "Забота о здоровье: питание, сон, профилактика, врачи",
                Slug = "health"
            },
            new Category
            {
                Id = 4,
                Name = "Образование",
                Description = "Образование и обучение: детский сад, школа, дополнительные занятия",
                Slug = "education"
            }
        );
    }

    private static void SeedArticles(ModelBuilder modelBuilder)
    {
        var now = DateTime.UtcNow;

        modelBuilder.Entity<Article>().HasData(
            new Article
            {
                Id = 1,
                Title = "Как справиться с эмоциональным выгоранием",
                Summary = "Практические советы для мам в декрете: как восстановить силы и найти баланс",
                Content = """
                    Эмоциональное выгорание — частая проблема молодых родителей. 
                    Вот 5 шагов, которые помогут восстановить энергию:
                    
                    1. Признайте свои чувства — это нормально чувствовать усталость.
                    2. Найдите 15 минут в день только для себя.
                    3. Делегируйте задачи партнёру или близким.
                    4. Не стремитесь к идеалу — «достаточно хорошо» тоже отлично.
                    5. Обратитесь за поддержкой: форум, психолог, группа родителей.
                    
                    Помните: забота о себе — это не эгоизм, а необходимость.
                    """,
                Slug = "burnout-help",
                ImageUrl = "/img/articles/burnout.jpg",
                Author = "Анна Петрова",
                CategoryId = 1,
                PublishedAt = now.AddDays(-5),
                CreatedAt = now.AddDays(-6),
                IsPublished = true
            },
            new Article
            {
                Id = 2,
                Title = "Развитие речи у детей: этапы от 0 до 3 лет",
                Summary = "Что должен уметь ребёнок в каждом возрасте и как помочь развитию речи",
                Content = """
                    Развитие речи — ключевой навык раннего детства.
                    
                    🗣️ 0–6 месяцев: гуление, реакция на голос
                    🗣️ 6–12 месяцев: лепет, первые слоги «ма-ма», «ба-ба»
                    🗣️ 1–2 года: первые слова, простые фразы из 2 слов
                    🗣️ 2–3 года: предложения из 3–5 слов, вопросы «почему?»
                    
                    Как помочь:
                    • Много разговаривайте с ребёнком
                    • Читайте книги с картинками
                    • Повторяйте и расширяйте его фразы
                    • Не исправляйте резко — моделируйте правильный вариант
                    """,
                Slug = "speech-development",
                ImageUrl = "/img/articles/speech.jpg",
                Author = "Мария Иванова",
                CategoryId = 2,
                PublishedAt = now.AddDays(-3),
                CreatedAt = now.AddDays(-4),
                IsPublished = true
            },
            new Article
            {
                Id = 3,
                Title = "Здоровый сон ребёнка: правила и ритуалы",
                Summary = "Как наладить режим сна и почему это важно для развития",
                Content = "Полный текст статьи о здоровом сне детей...",
                Slug = "healthy-sleep",
                ImageUrl = "/img/articles/sleep.jpg",
                Author = "Елена Смирнова",
                CategoryId = 3,
                PublishedAt = now.AddDays(-1),
                CreatedAt = now.AddDays(-2),
                IsPublished = true
            }
        );
    }

    private static void SeedSpecialists(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Specialist>().HasData(
            new Specialist
            {
                Id = 1,
                FirstName = "Елена",
                LastName = "Смирнова",
                Specialization = "Детский психолог",
                Description = "Специалист по работе с детьми дошкольного возраста. Помогаю при тревожности, страхах, адаптации к детскому саду. Использую игровые методики и арт-терапию.",
                ExperienceYears = 10,
                PricePerHour = 2500,
                PhotoUrl = "/img/specialists/smirnova.jpg",
                IsAvailable = true
            },
            new Specialist
            {
                Id = 2,
                FirstName = "Дмитрий",
                LastName = "Козлов",
                Specialization = "Семейный психолог",
                Description = "Помогаю решить семейные конфликты, наладить коммуникацию между родителями и детьми, пережить кризисные периоды. Работаю в направлении системной семейной терапии.",
                ExperienceYears = 8,
                PricePerHour = 3000,
                PhotoUrl = "/img/specialists/kozlov.jpg",
                IsAvailable = true
            },
            new Specialist
            {
                Id = 3,
                FirstName = "Ольга",
                LastName = "Волкова",
                Specialization = "Логопед-дефектолог",
                Description = "Коррекция речевых нарушений у детей от 3 до 10 лет. Постановка звуков, развитие связной речи, подготовка к школе.",
                ExperienceYears = 12,
                PricePerHour = 2200,
                PhotoUrl = "/img/specialists/volkova.jpg",
                IsAvailable = false
            }
        );
    }

    private static void SeedForumTopics(ModelBuilder modelBuilder)
    {
        var now = DateTime.UtcNow;

        modelBuilder.Entity<ForumTopic>().HasData(
            new ForumTopic
            {
                Id = 1,
                Title = "Знакомство",
                Description = "Расскажите о себе: как вас зовут, сколько детям, откуда вы",
                CreatedAt = now.AddDays(-10),
                PostCount = 24
            },
            new ForumTopic
            {
                Id = 2,
                Title = "Вопросы психологу",
                Description = "Задайте вопрос специалисту — ответим в течение 24 часов",
                CreatedAt = now.AddDays(-7),
                PostCount = 15
            },
            new ForumTopic
            {
                Id = 3,
                Title = "Мамы в декрете",
                Description = "Общение, поддержка, обмен лайфхаками для мам в отпуске по уходу за ребёнком",
                CreatedAt = now.AddDays(-5),
                PostCount = 42
            },
            new ForumTopic
            {
                Id = 4,
                Title = "Детский сад: адаптация и советы",
                Description = "Как подготовить ребёнка к саду, пережить первый день, решить проблемы с воспитателями",
                CreatedAt = now.AddDays(-2),
                PostCount = 8
            }
        );
    }

    #endregion
}