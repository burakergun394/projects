using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonnelTransport.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "ShiftEnd", "ShiftStart", "AssignedRouteId", "FirstName", "LastName", "SpecialNeeds", "HomeLatitude", "HomeLongitude" },
                values: new object[,]
                {
                    { new Guid("018cc778-5000-7d82-86c1-d960f8009ec9"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee1", "Surname1", "Wheelchair", 41.0, 29.0 },
                    { new Guid("018cc779-3a60-7253-af07-50b6e36ba3bd"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee2", "Surname2", "", 41.005000000000003, 29.004999999999999 },
                    { new Guid("018cc77a-24c0-7f5b-bb5d-d1e5a6e27bb8"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee3", "Surname3", "", 41.009999999999998, 29.010000000000002 },
                    { new Guid("018cc77b-0f20-78d1-a1a5-fb39e5f12aaa"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee4", "Surname4", "", 41.015000000000001, 29.015000000000001 },
                    { new Guid("018cc77b-f980-7de5-bc88-fb781a63bfab"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee5", "Surname5", "", 41.020000000000003, 29.02 },
                    { new Guid("018cc77c-e3e0-720c-977e-847ea9e35c09"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee6", "Surname6", "", 41.024999999999999, 29.024999999999999 },
                    { new Guid("018cc77d-ce40-7378-ae49-cb480687111a"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee7", "Surname7", "", 41.030000000000001, 29.030000000000001 },
                    { new Guid("018cc77e-b8a0-72ed-b418-c589ec0a0f1a"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee8", "Surname8", "", 41.034999999999997, 29.035 },
                    { new Guid("018cc77f-a300-7d70-835d-b8fd731be9ce"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee9", "Surname9", "", 41.039999999999999, 29.039999999999999 },
                    { new Guid("018cc780-8d60-7b97-be9b-abaf7939e218"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee10", "Surname10", "", 41.045000000000002, 29.045000000000002 },
                    { new Guid("018cc781-77c0-7703-8cd1-04b3c2f8ec37"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee11", "Surname11", "Wheelchair", 41.049999999999997, 29.050000000000001 },
                    { new Guid("018cc782-6220-7fca-b017-e7b11dcdf784"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee12", "Surname12", "", 41.055, 29.055 },
                    { new Guid("018cc783-4c80-7878-afe8-469d22dfcf4d"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee13", "Surname13", "", 41.060000000000002, 29.059999999999999 },
                    { new Guid("018cc784-36e0-74db-a4e1-e9f84b1450d6"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee14", "Surname14", "", 41.064999999999998, 29.065000000000001 },
                    { new Guid("018cc785-2140-74df-917f-e858d091562f"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee15", "Surname15", "", 41.07, 29.07 },
                    { new Guid("018cc786-0ba0-7808-994e-cbf7da4d11c2"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee16", "Surname16", "", 41.075000000000003, 29.074999999999999 },
                    { new Guid("018cc786-f600-78f5-ac72-ffa8cea964d4"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee17", "Surname17", "", 41.079999999999998, 29.079999999999998 },
                    { new Guid("018cc787-e060-7576-8908-f62afcc0f80f"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee18", "Surname18", "", 41.085000000000001, 29.085000000000001 },
                    { new Guid("018cc788-cac0-71b3-a751-9ac677b70af5"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee19", "Surname19", "", 41.090000000000003, 29.09 },
                    { new Guid("018cc789-b520-7009-979c-aa76680ec4bd"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee20", "Surname20", "", 41.094999999999999, 29.094999999999999 },
                    { new Guid("018cc78a-9f80-7117-bb59-5a7515d384c5"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee21", "Surname21", "Wheelchair", 41.100000000000001, 29.100000000000001 },
                    { new Guid("018cc78b-89e0-753e-a5f1-ec1b31dde02b"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee22", "Surname22", "", 41.104999999999997, 29.105 },
                    { new Guid("018cc78c-7440-72cb-8e06-0da07c0e1def"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee23", "Surname23", "", 41.109999999999999, 29.109999999999999 },
                    { new Guid("018cc78d-5ea0-7a7b-b39f-fe4586d1029a"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee24", "Surname24", "", 41.115000000000002, 29.114999999999998 },
                    { new Guid("018cc78e-4900-721e-9a3c-3257fd4a426a"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee25", "Surname25", "", 41.119999999999997, 29.120000000000001 },
                    { new Guid("018cc78f-3360-7747-8a42-82c0634dcc84"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee26", "Surname26", "", 41.125, 29.125 },
                    { new Guid("018cc790-1dc0-7b84-b908-00ab5e634480"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee27", "Surname27", "", 41.130000000000003, 29.129999999999999 },
                    { new Guid("018cc791-0820-76c9-9574-6650b92d93d9"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee28", "Surname28", "", 41.134999999999998, 29.135000000000002 },
                    { new Guid("018cc791-f280-7d6e-9152-b9221012ea7a"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee29", "Surname29", "", 41.140000000000001, 29.140000000000001 },
                    { new Guid("018cc792-dce0-75e5-a525-1b635469ffc3"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee30", "Surname30", "", 41.145000000000003, 29.145 },
                    { new Guid("018cc793-c740-7be6-a813-8992ae9dd30e"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee31", "Surname31", "Wheelchair", 41.149999999999999, 29.149999999999999 },
                    { new Guid("018cc794-b1a0-71a1-992f-71703003889c"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee32", "Surname32", "", 41.155000000000001, 29.155000000000001 },
                    { new Guid("018cc795-9c00-7590-b314-81edcf8206d7"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee33", "Surname33", "", 41.159999999999997, 29.16 },
                    { new Guid("018cc796-8660-7987-a6b1-d3daa9f7d527"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee34", "Surname34", "", 41.164999999999999, 29.164999999999999 },
                    { new Guid("018cc797-70c0-704a-bd2f-97e8952dacdf"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee35", "Surname35", "", 41.170000000000002, 29.170000000000002 },
                    { new Guid("018cc798-5b20-73ed-a8b9-51011c3fed84"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee36", "Surname36", "", 41.174999999999997, 29.175000000000001 },
                    { new Guid("018cc799-4580-7dba-934c-58920953b651"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee37", "Surname37", "", 41.18, 29.18 },
                    { new Guid("018cc79a-2fe0-718f-b110-17c07eead2e1"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee38", "Surname38", "", 41.185000000000002, 29.184999999999999 },
                    { new Guid("018cc79b-1a40-7e6b-8b80-e4e3f547a340"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee39", "Surname39", "", 41.189999999999998, 29.190000000000001 },
                    { new Guid("018cc79c-04a0-7331-b3a8-14e77eb5e157"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee40", "Surname40", "", 41.195, 29.195 },
                    { new Guid("018cc79c-ef00-7efd-9b41-b3e78712d59f"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee41", "Surname41", "Wheelchair", 41.200000000000003, 29.199999999999999 },
                    { new Guid("018cc79d-d960-7566-9502-c5c041f21d5c"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee42", "Surname42", "", 41.204999999999998, 29.204999999999998 },
                    { new Guid("018cc79e-c3c0-73d3-9442-7025bb47632a"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee43", "Surname43", "", 41.210000000000001, 29.210000000000001 },
                    { new Guid("018cc79f-ae20-7279-8349-e4e78337160e"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee44", "Surname44", "", 41.215000000000003, 29.215 },
                    { new Guid("018cc7a0-9880-7563-a029-bbf1b766c591"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee45", "Surname45", "", 41.219999999999999, 29.219999999999999 },
                    { new Guid("018cc7a1-82e0-717c-98a7-47624b4a40d9"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee46", "Surname46", "", 41.225000000000001, 29.225000000000001 },
                    { new Guid("018cc7a2-6d40-7038-a66e-4f7b65aa80a7"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee47", "Surname47", "", 41.229999999999997, 29.23 },
                    { new Guid("018cc7a3-57a0-7592-9278-4904dfaeeafb"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee48", "Surname48", "", 41.234999999999999, 29.234999999999999 },
                    { new Guid("018cc7a4-4200-7d3c-b73c-a4df3ad1172d"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee49", "Surname49", "", 41.240000000000002, 29.239999999999998 },
                    { new Guid("018cc7a5-2c60-7cb8-a608-19883c562937"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee50", "Surname50", "", 41.244999999999997, 29.245000000000001 },
                    { new Guid("018cc7a6-16c0-7677-849b-36be46e9ec14"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee51", "Surname51", "Wheelchair", 41.25, 29.25 },
                    { new Guid("018cc7a7-0120-729a-a6c9-e7b4a4dacdac"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee52", "Surname52", "", 41.255000000000003, 29.254999999999999 },
                    { new Guid("018cc7a7-eb80-78ce-a079-8f3e3069afbf"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee53", "Surname53", "", 41.259999999999998, 29.260000000000002 },
                    { new Guid("018cc7a8-d5e0-7044-8b19-2e25e7af82a7"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee54", "Surname54", "", 41.265000000000001, 29.265000000000001 },
                    { new Guid("018cc7a9-c040-76e3-a077-e937c59469c0"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee55", "Surname55", "", 41.270000000000003, 29.27 },
                    { new Guid("018cc7aa-aaa0-7e66-95de-2c1a89ae5fd6"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee56", "Surname56", "", 41.274999999999999, 29.274999999999999 },
                    { new Guid("018cc7ab-9500-7530-a867-82321f94dceb"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee57", "Surname57", "", 41.280000000000001, 29.280000000000001 },
                    { new Guid("018cc7ac-7f60-7f2b-9810-fe8b5094fc24"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee58", "Surname58", "", 41.284999999999997, 29.285 },
                    { new Guid("018cc7ad-69c0-78c9-b57a-e33446f77e8f"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee59", "Surname59", "", 41.289999999999999, 29.289999999999999 },
                    { new Guid("018cc7ae-5420-736e-a1ba-7ec6d3b9cf13"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee60", "Surname60", "", 41.295000000000002, 29.295000000000002 },
                    { new Guid("018cc7af-3e80-730f-9d3c-4d6800096d6e"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee61", "Surname61", "Wheelchair", 41.299999999999997, 29.300000000000001 },
                    { new Guid("018cc7b0-28e0-7bfc-9a64-f48af8852460"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee62", "Surname62", "", 41.305, 29.305 },
                    { new Guid("018cc7b1-1340-7583-9639-bb84a124b67d"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee63", "Surname63", "", 41.310000000000002, 29.309999999999999 },
                    { new Guid("018cc7b1-fda0-784e-ad72-79724cba6add"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee64", "Surname64", "", 41.314999999999998, 29.315000000000001 },
                    { new Guid("018cc7b2-e800-7e3d-ae92-4339964d2148"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee65", "Surname65", "", 41.32, 29.32 },
                    { new Guid("018cc7b3-d260-7913-ae93-1692a20da195"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee66", "Surname66", "", 41.325000000000003, 29.324999999999999 },
                    { new Guid("018cc7b4-bcc0-74a0-a4fd-f3688e29d486"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee67", "Surname67", "", 41.329999999999998, 29.329999999999998 },
                    { new Guid("018cc7b5-a720-7359-a67f-435155dcdef7"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee68", "Surname68", "", 41.335000000000001, 29.335000000000001 },
                    { new Guid("018cc7b6-9180-74b2-b75f-a94e4f2f3fde"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee69", "Surname69", "", 41.340000000000003, 29.34 },
                    { new Guid("018cc7b7-7be0-75e1-8f7e-7d7316d1a570"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee70", "Surname70", "", 41.344999999999999, 29.344999999999999 },
                    { new Guid("018cc7b8-6640-7721-87d8-e19aeed71f2f"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee71", "Surname71", "Wheelchair", 41.350000000000001, 29.350000000000001 },
                    { new Guid("018cc7b9-50a0-79a5-bd9e-30c98193a323"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee72", "Surname72", "", 41.354999999999997, 29.355 },
                    { new Guid("018cc7ba-3b00-72ab-88e0-d41fea66e432"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee73", "Surname73", "", 41.359999999999999, 29.359999999999999 },
                    { new Guid("018cc7bb-2560-77d2-afa8-d410642dda10"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee74", "Surname74", "", 41.365000000000002, 29.364999999999998 },
                    { new Guid("018cc7bc-0fc0-7ad7-bcdf-760a87849ce3"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee75", "Surname75", "", 41.369999999999997, 29.370000000000001 },
                    { new Guid("018cc7bc-fa20-75bb-b729-5ffa11014d48"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee76", "Surname76", "", 41.375, 29.375 },
                    { new Guid("018cc7bd-e480-795c-a343-242f4f68f70f"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee77", "Surname77", "", 41.380000000000003, 29.379999999999999 },
                    { new Guid("018cc7be-cee0-728f-a276-355aef800743"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee78", "Surname78", "", 41.384999999999998, 29.385000000000002 },
                    { new Guid("018cc7bf-b940-7985-a2b1-57cfd318a37b"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee79", "Surname79", "", 41.390000000000001, 29.390000000000001 },
                    { new Guid("018cc7c0-a3a0-7fe3-b805-697a242600ad"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee80", "Surname80", "", 41.395000000000003, 29.395 },
                    { new Guid("018cc7c1-8e00-79f2-add0-bf1efe36fc3b"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee81", "Surname81", "Wheelchair", 41.399999999999999, 29.399999999999999 },
                    { new Guid("018cc7c2-7860-7b0e-a7f2-414540cb4372"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee82", "Surname82", "", 41.405000000000001, 29.405000000000001 },
                    { new Guid("018cc7c3-62c0-79c6-b252-5c3a3e5baf89"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee83", "Surname83", "", 41.409999999999997, 29.41 },
                    { new Guid("018cc7c4-4d20-7a7a-af0c-3d9ca995ec3f"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee84", "Surname84", "", 41.414999999999999, 29.414999999999999 },
                    { new Guid("018cc7c5-3780-71da-b005-400a1ef834d9"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee85", "Surname85", "", 41.420000000000002, 29.420000000000002 },
                    { new Guid("018cc7c6-21e0-70f5-bc7e-444a200d9818"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee86", "Surname86", "", 41.424999999999997, 29.425000000000001 },
                    { new Guid("018cc7c7-0c40-7950-8560-23d8ae728deb"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee87", "Surname87", "", 41.43, 29.43 },
                    { new Guid("018cc7c7-f6a0-7e56-90c0-806901fec74a"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee88", "Surname88", "", 41.435000000000002, 29.434999999999999 },
                    { new Guid("018cc7c8-e100-745e-901b-222448a1cbee"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee89", "Surname89", "", 41.439999999999998, 29.440000000000001 },
                    { new Guid("018cc7c9-cb60-7f32-9082-c8cd4f587c78"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee90", "Surname90", "", 41.445, 29.445 },
                    { new Guid("018cc7ca-b5c0-7201-939f-34957025f111"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee91", "Surname91", "Wheelchair", 41.450000000000003, 29.449999999999999 },
                    { new Guid("018cc7cb-a020-741a-a7b9-60902b78d927"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee92", "Surname92", "", 41.454999999999998, 29.454999999999998 },
                    { new Guid("018cc7cc-8a80-7dcb-9770-ca2a66f467e1"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee93", "Surname93", "", 41.460000000000001, 29.460000000000001 },
                    { new Guid("018cc7cd-74e0-7646-9e34-09c0298c5ab0"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee94", "Surname94", "", 41.465000000000003, 29.465 },
                    { new Guid("018cc7ce-5f40-7d6b-85e0-dc63a689b348"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee95", "Surname95", "", 41.469999999999999, 29.469999999999999 },
                    { new Guid("018cc7cf-49a0-710f-81bd-2a470c83e6f9"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee96", "Surname96", "", 41.475000000000001, 29.475000000000001 },
                    { new Guid("018cc7d0-3400-7b5e-9034-4699b87b24b6"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee97", "Surname97", "", 41.479999999999997, 29.48 },
                    { new Guid("018cc7d1-1e60-7f05-a717-551463fc15fb"), new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), null, "Employee98", "Surname98", "", 41.484999999999999, 29.484999999999999 },
                    { new Guid("018cc7d2-08c0-781b-812b-ac20d4b0f3e3"), new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 10, 0, 0, 0), null, "Employee99", "Surname99", "", 41.490000000000002, 29.489999999999998 },
                    { new Guid("018cc7d2-f320-7250-996c-c6d796c7389f"), new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0), null, "Employee100", "Surname100", "", 41.494999999999997, 29.495000000000001 }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Capacity", "CostPerKm", "PlateNumber", "Type", "DepotLatitude", "DepotLongitude" },
                values: new object[,]
                {
                    { new Guid("018cc251-f400-79bc-9e3b-98aba0364386"), 15, 5.0, "34 ABC 100", "Bus", 41.0, 29.0 },
                    { new Guid("018cc252-de60-7e0a-970d-cfbe37847dfa"), 17, 5.5, "34 ABC 101", "Bus", 41.009999999999998, 29.010000000000002 },
                    { new Guid("018cc253-c8c0-7a15-b367-1da07a2e8bc5"), 19, 6.0, "34 ABC 102", "Bus", 41.020000000000003, 29.02 },
                    { new Guid("018cc254-b320-7250-991a-b6488c859b27"), 21, 6.5, "34 ABC 103", "Bus", 41.030000000000001, 29.030000000000001 },
                    { new Guid("018cc255-9d80-79ff-ac10-65424d6ef982"), 23, 7.0, "34 ABC 104", "Bus", 41.039999999999999, 29.039999999999999 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc778-5000-7d82-86c1-d960f8009ec9"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc779-3a60-7253-af07-50b6e36ba3bd"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc77a-24c0-7f5b-bb5d-d1e5a6e27bb8"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc77b-0f20-78d1-a1a5-fb39e5f12aaa"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc77b-f980-7de5-bc88-fb781a63bfab"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc77c-e3e0-720c-977e-847ea9e35c09"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc77d-ce40-7378-ae49-cb480687111a"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc77e-b8a0-72ed-b418-c589ec0a0f1a"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc77f-a300-7d70-835d-b8fd731be9ce"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc780-8d60-7b97-be9b-abaf7939e218"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc781-77c0-7703-8cd1-04b3c2f8ec37"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc782-6220-7fca-b017-e7b11dcdf784"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc783-4c80-7878-afe8-469d22dfcf4d"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc784-36e0-74db-a4e1-e9f84b1450d6"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc785-2140-74df-917f-e858d091562f"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc786-0ba0-7808-994e-cbf7da4d11c2"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc786-f600-78f5-ac72-ffa8cea964d4"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc787-e060-7576-8908-f62afcc0f80f"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc788-cac0-71b3-a751-9ac677b70af5"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc789-b520-7009-979c-aa76680ec4bd"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc78a-9f80-7117-bb59-5a7515d384c5"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc78b-89e0-753e-a5f1-ec1b31dde02b"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc78c-7440-72cb-8e06-0da07c0e1def"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc78d-5ea0-7a7b-b39f-fe4586d1029a"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc78e-4900-721e-9a3c-3257fd4a426a"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc78f-3360-7747-8a42-82c0634dcc84"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc790-1dc0-7b84-b908-00ab5e634480"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc791-0820-76c9-9574-6650b92d93d9"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc791-f280-7d6e-9152-b9221012ea7a"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc792-dce0-75e5-a525-1b635469ffc3"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc793-c740-7be6-a813-8992ae9dd30e"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc794-b1a0-71a1-992f-71703003889c"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc795-9c00-7590-b314-81edcf8206d7"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc796-8660-7987-a6b1-d3daa9f7d527"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc797-70c0-704a-bd2f-97e8952dacdf"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc798-5b20-73ed-a8b9-51011c3fed84"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc799-4580-7dba-934c-58920953b651"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc79a-2fe0-718f-b110-17c07eead2e1"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc79b-1a40-7e6b-8b80-e4e3f547a340"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc79c-04a0-7331-b3a8-14e77eb5e157"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc79c-ef00-7efd-9b41-b3e78712d59f"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc79d-d960-7566-9502-c5c041f21d5c"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc79e-c3c0-73d3-9442-7025bb47632a"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc79f-ae20-7279-8349-e4e78337160e"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7a0-9880-7563-a029-bbf1b766c591"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7a1-82e0-717c-98a7-47624b4a40d9"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7a2-6d40-7038-a66e-4f7b65aa80a7"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7a3-57a0-7592-9278-4904dfaeeafb"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7a4-4200-7d3c-b73c-a4df3ad1172d"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7a5-2c60-7cb8-a608-19883c562937"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7a6-16c0-7677-849b-36be46e9ec14"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7a7-0120-729a-a6c9-e7b4a4dacdac"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7a7-eb80-78ce-a079-8f3e3069afbf"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7a8-d5e0-7044-8b19-2e25e7af82a7"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7a9-c040-76e3-a077-e937c59469c0"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7aa-aaa0-7e66-95de-2c1a89ae5fd6"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7ab-9500-7530-a867-82321f94dceb"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7ac-7f60-7f2b-9810-fe8b5094fc24"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7ad-69c0-78c9-b57a-e33446f77e8f"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7ae-5420-736e-a1ba-7ec6d3b9cf13"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7af-3e80-730f-9d3c-4d6800096d6e"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7b0-28e0-7bfc-9a64-f48af8852460"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7b1-1340-7583-9639-bb84a124b67d"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7b1-fda0-784e-ad72-79724cba6add"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7b2-e800-7e3d-ae92-4339964d2148"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7b3-d260-7913-ae93-1692a20da195"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7b4-bcc0-74a0-a4fd-f3688e29d486"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7b5-a720-7359-a67f-435155dcdef7"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7b6-9180-74b2-b75f-a94e4f2f3fde"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7b7-7be0-75e1-8f7e-7d7316d1a570"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7b8-6640-7721-87d8-e19aeed71f2f"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7b9-50a0-79a5-bd9e-30c98193a323"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7ba-3b00-72ab-88e0-d41fea66e432"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7bb-2560-77d2-afa8-d410642dda10"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7bc-0fc0-7ad7-bcdf-760a87849ce3"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7bc-fa20-75bb-b729-5ffa11014d48"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7bd-e480-795c-a343-242f4f68f70f"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7be-cee0-728f-a276-355aef800743"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7bf-b940-7985-a2b1-57cfd318a37b"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7c0-a3a0-7fe3-b805-697a242600ad"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7c1-8e00-79f2-add0-bf1efe36fc3b"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7c2-7860-7b0e-a7f2-414540cb4372"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7c3-62c0-79c6-b252-5c3a3e5baf89"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7c4-4d20-7a7a-af0c-3d9ca995ec3f"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7c5-3780-71da-b005-400a1ef834d9"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7c6-21e0-70f5-bc7e-444a200d9818"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7c7-0c40-7950-8560-23d8ae728deb"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7c7-f6a0-7e56-90c0-806901fec74a"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7c8-e100-745e-901b-222448a1cbee"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7c9-cb60-7f32-9082-c8cd4f587c78"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7ca-b5c0-7201-939f-34957025f111"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7cb-a020-741a-a7b9-60902b78d927"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7cc-8a80-7dcb-9770-ca2a66f467e1"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7cd-74e0-7646-9e34-09c0298c5ab0"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7ce-5f40-7d6b-85e0-dc63a689b348"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7cf-49a0-710f-81bd-2a470c83e6f9"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7d0-3400-7b5e-9034-4699b87b24b6"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7d1-1e60-7f05-a717-551463fc15fb"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7d2-08c0-781b-812b-ac20d4b0f3e3"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("018cc7d2-f320-7250-996c-c6d796c7389f"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("018cc251-f400-79bc-9e3b-98aba0364386"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("018cc252-de60-7e0a-970d-cfbe37847dfa"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("018cc253-c8c0-7a15-b367-1da07a2e8bc5"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("018cc254-b320-7250-991a-b6488c859b27"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("018cc255-9d80-79ff-ac10-65424d6ef982"));
        }
    }
}
