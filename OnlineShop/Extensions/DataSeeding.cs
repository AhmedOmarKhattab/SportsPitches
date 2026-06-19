using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FiveStadium.Data;
using FiveStadium.Extensions;
using FiveStadium.Models;

namespace BrightMinds.Api.Extensions
{
    public static class DataSeeding
    {
        public static async Task<WebApplication> SeedAppData(this WebApplication application, ApplicationDbContext context)
        {
           
          
            // 1. Seed Special Tags
            if (!context.specialTags.Any())
            {
                var specialTags = new List<SpecialTag>
                {
                    new SpecialTag { Name = "جديد" },
                    new SpecialTag { Name = "الأكثر مبيعًا" },
                    new SpecialTag { Name = "عرض خاص" },
                    new SpecialTag { Name = "مناسب للهدايا" }
                };
                await context.AddRangeAsync(specialTags);
                await context.SaveChangesAsync();
            }

            // 2. Seed Product Types (Toy Categories)
            //var p = await context.PitchTypes.ToListAsync();
            //context.RemoveRange(p);
            //await context.SaveChangesAsync();

            if (!context.PitchTypes.Any())
            {
                var productTypes = new List<PitchType>
                 {
                     new PitchType {  Name = "ملعب خماسي (نجيل صناعي)" },
                     new PitchType {  Name = "ملعب سباعي (نجيل صناعي)" },
                     new PitchType {  Name = "ملعب قانوني (نجيل طبيعي)" },
                     new PitchType {  Name = "ملعب خماسي (مغطى/صالات)" },
                     new PitchType {  Name = "ملعب بادل (Padel)" },
                     new PitchType {  Name = "ملعب متعدد (سلة/طائرة)" }
                 };
                await context.AddRangeAsync(productTypes);
                await context.SaveChangesAsync();
            }

            // 6. Seed Brands (Toy Brands)
            // 4. Seed Products (Kids Toys)

            //var c=await context.Pitches
            //    .ToListAsync();
            //context.RemoveRange(c);
            //await context.SaveChangesAsync();

            if (!context.Pitches.Any())
            {
                var tagIds = await context.specialTags.Select(t => t.Id).ToListAsync();
                var typeIds = await context.PitchTypes.Select(t => t.Id).ToListAsync();
                var random = new Random();


                var pitches = new List<Pitch>
{
    new Pitch
    {
        Name = "ملعب ماركانا - التجمع",
        Price = 450.00m,
        Image = "image1.jpg",
        IsAvailable = true,
        SpecialTagId=tagIds[4%tagIds.Count],
 PitchTypeId = typeIds[random.Next(typeIds.Count)],
     
        Description = "نجيل صناعي تركي درجة أولى، كشافات ليد قوية، وغرف تغيير ملابس.",
        Address = "شارع التسعين الجنوبي، خلف الجامعة الأمريكية، القاهرة الجديدة"
    },
    new Pitch
    {
        Name = "ساحة الأبطال - سموحة",
        Price = 650.00m,
        Image = "image2.jpg",
        IsAvailable = true,
         SpecialTagId=tagIds[4%tagIds.Count],
 PitchTypeId = typeIds[random.Next(typeIds.Count)],
        
        Description = "ملعب واسع مناسب للمباريات الكبيرة، متاح جراج خاص للسيارات.",
        Address = "بجوار نادي سموحة، الإسكندرية"
    },
    new Pitch
    {
        Name = "ملعب روف توب - الدقي",
        Price = 600.00m,
        Image = "image6.jpg",
        IsAvailable = false,
         SpecialTagId=tagIds[4%tagIds.Count],
 PitchTypeId = typeIds[random.Next(typeIds.Count)],
         Description = "تجربة فريدة للعب فوق أسطح العمارات مع إطلالة على النيل.",
        Address = "شارع مصدق، فوق مول المدينة، الدقي، الجيزة"
    },
    new Pitch
    {
        Name = "مركز شباب الجزيرة",
        Price = 250.00m,
        Image = "image4.jpg",
        IsAvailable = true,
         SpecialTagId=tagIds[4%tagIds.Count],
 PitchTypeId = typeIds[random.Next(typeIds.Count)],
                      
        Description = "أرضية معتمدة من الفيفا، متاح طوال أيام الأسبوع.",
        Address = "جزيرة الزمالك، أمام برج القاهرة"
    },
    new Pitch
    {
        Name = "ستاد المنصورة الفرعي",
        Price = 200.00m,
        Image = "image1.jpg",
        IsAvailable = true,
        SpecialTagId=tagIds[4%tagIds.Count],
PitchTypeId = typeIds[random.Next(typeIds.Count)],
     
        Description = "ملعب قانوني كبير للفرق والتدريبات الرسمية.",
        Address = "شارع عبد السلام عارف، المنصورة، الدقهلية"
    },
    new Pitch
    {
        Name = "ملعب الوفاء - مدينة نصر",
        Price = 400.00m,
        Image = "image1.jpg",
        IsAvailable = true,
        SpecialTagId=tagIds[4%tagIds.Count],
PitchTypeId = typeIds[random.Next(typeIds.Count)],
     
        Description = "يتميز بوجود كافيتريا ومنطقة انتظار مكيفة للمرافقين.",
        Address = "الحي الثامن، بجوار مدرسة المنهل، مدينة نصر"
    },
    new Pitch
    {
        Name = "سوبر سبورت - طنطا",
        Price = 280.00m,
        Image = "image2.jpg",
        IsAvailable = true,
        SpecialTagId=tagIds[4%tagIds.Count],
PitchTypeId = typeIds[random.Next(typeIds.Count)],
     
        Description = "نجيل هجين (خليط) ومساحات واسعة حول الملعب.",
        Address = "طريق طنطا الإسكندرية الزراعي، طنطا"
    },
    new Pitch
    {
        Name = "ملعب الفرسان - المعادي",
        Price = 500.00m,
        Image = "image4.jpg",
        IsAvailable = true,
        SpecialTagId=tagIds[4%tagIds.Count],
PitchTypeId = typeIds[random.Next(typeIds.Count)],
     
        Description = "ملعب سباعي فاخر، إضاءة ليلية ممتازة وتجهيزات فندقية.",
        Address = "المعادي الجديدة، شارع النصر"
    },
    new Pitch
    {
        Name = "ملعب الشروق هاب",
        Price = 620.00m,
        Image = "image4.jpg",
        IsAvailable = true,
        SpecialTagId=tagIds[4%tagIds.Count],
PitchTypeId = typeIds[random.Next(typeIds.Count)],
     
        Description = "ملعب جديد كلياً، مناسب جداً للشباب والأطفال.",
        Address = "مدينة الشروق، منطقة النوادي"
    },
     new Pitch
    {
        Name = "ملعب الشروق هاب",
        Price = 620.00m,
        Image = "image4.jpg",
        IsAvailable = true,
        SpecialTagId=tagIds[4%tagIds.Count],
PitchTypeId = typeIds[random.Next(typeIds.Count)],
     
        Description = "ملعب جديد كلياً، مناسب جداً للشباب والأطفال.",
        Address = "مدينة الشروق، منطقة النوادي"
    },
 new Pitch
    {
        Name = "ملعب الشروق هاب",
        Price = 620.00m,
        Image = "image4.jpg",
        IsAvailable = true,
        SpecialTagId=tagIds[4%tagIds.Count],
PitchTypeId = typeIds[random.Next(typeIds.Count)],
     
        Description = "ملعب جديد كلياً، مناسب جداً للشباب والأطفال.",
        Address = "مدينة الشروق، منطقة النوادي"
    },

    new Pitch
    {
        Name = "ملعب النجوم - أسيوط",
        Price = 180.00m,
        Image = "image2.jpg",
        IsAvailable = true,
        SpecialTagId=tagIds[4%tagIds.Count],
PitchTypeId = typeIds[random.Next(typeIds.Count)],
     
        Description = "أفضل سعر في المنطقة مع مرافق جيدة جداً.",
        Address = "حي شرق، خلف جامعة أسيوط"
    }
};

                context.AddRange(pitches);
                await context.SaveChangesAsync();
            }
            if (!context.Appointments.Any())
            {
                var pitchesIds = await context.Pitches.Select(c => c.Id).ToListAsync();
                var appointmentsToCreate = new List<PitchAppointment>();

                // Set the date for the appointments (e.g., Today)
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);

                foreach (var pitchId in pitchesIds)
                {
                    // Example: Generating hourly slots from 16:00 (4 PM) to 22:00 (10 PM)
                    for (int hour = 16; hour < 22; hour++)
                    {
                        appointmentsToCreate.Add(new PitchAppointment
                        {
                            Date = today,
                            StartDate = new TimeSpan(hour, 0, 0),       // e.g., 16:00:00
                            EndDate = new TimeSpan(hour + 1, 0, 0),    // e.g., 17:00:00
                            IsAvailable = true,
                            PitchId = pitchId
                            // Pitch navigation property can be left null; EF handles it via PitchId
                        });
                    }
                }

                // Add all generated slots to the database in one optimized batch
                await context.Appointments.AddRangeAsync(appointmentsToCreate);
                await context.SaveChangesAsync();
            }


            return application;
        }
    }
}