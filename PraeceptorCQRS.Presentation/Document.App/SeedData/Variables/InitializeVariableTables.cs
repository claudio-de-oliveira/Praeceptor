using Ardalis.GuardClauses;

using Document.App.Interfaces;
using Document.App.Models;

using Newtonsoft.Json;

namespace Document.App.SeedData.Variables
{
    public static class InitializeVariableTables
    {
        public static async Task Initialize(
            Guid instituteId,
            IGroupService groupService,
            IGroupValueService groupValueService,
            IVariableService variableService,
            IVariableValueService variableValueService
            )
        {
            var exist = await groupService.Exists(instituteId, "Holding");

            GroupModel? group;
            GroupValueModel? setGroupValue;
            GroupValueModel? mecGroupValue;
            VariableModel? address;
            VariableModel? phone;

            if (!exist)
            {
                group = await CreateGroup(groupService, instituteId, "Holding");
                Guard.Against.Null(group);
                address = await CreateVariable(variableService, group.Id, "ENDERECO");
                Guard.Against.Null(address);
                phone = await CreateVariable(variableService, group.Id, "TELEFONE");
                Guard.Against.Null(phone);

                setGroupValue = await CreateGroupValue(groupValueService, group.Id, "SET");
                Guard.Against.Null(setGroupValue);
                await CreateVariableValue(variableValueService, setGroupValue.Id, phone.Id, "+55 (79) 3218 2226");
                await CreateVariableValue(variableValueService, setGroupValue.Id, address.Id, "Av. Murilo Dantas, 300 - Farolândia 49032-490 Aracaju - Sergipe - Brasil");

                mecGroupValue = await CreateGroupValue(groupValueService, group.Id, "MEC");
                Guard.Against.Null(mecGroupValue);
                await CreateVariableValue(variableValueService, mecGroupValue.Id, phone.Id, "");
                await CreateVariableValue(variableValueService, mecGroupValue.Id, address.Id, "Esplanada dos Ministérios Ed. Sede e Anexos, BL L - Brasília, DF, 70047-900");
            }
        }

        private static async Task<VariableValueModel?> CreateVariableValue(IVariableValueService variableValueService, Guid groupValueId, Guid variableId, string val)
        {
            var response = await variableValueService.CreateVariableValue(
                new PraeceptorCQRS.Contracts.Entities.VariableValue.CreateVariableValueRequest(
                    groupValueId,
                    variableId,
                    val
                    )
                );

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<VariableValueModel>(await response.Content.ReadAsStringAsync());
            else
                return null;
        }

        private static async Task<GroupValueModel?> CreateGroupValue(IGroupValueService groupValueService, Guid groupId, string val)
        {
            var response = await groupValueService.CreateGroupValue(
                new PraeceptorCQRS.Contracts.Entities.GroupValue.CreateGroupValueRequest(
                    groupId,
                    val
                    )
                );

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<GroupValueModel>(await response.Content.ReadAsStringAsync());
            else
                return null;
        }

        private static async Task<VariableModel?> CreateVariable(IVariableService variableService, Guid groupId, string code)
        {
            var response = await variableService.CreateVariable(
                new PraeceptorCQRS.Contracts.Entities.Variable.CreateVariableRequest(
                    groupId,
                    code
                    )
                );

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<VariableModel>(await response.Content.ReadAsStringAsync());
            else
                return null;
        }

        private static async Task<GroupModel?> CreateGroup(IGroupService groupService, Guid instituteId, string code)
        {
            var response = await groupService.CreateGroup(
                new PraeceptorCQRS.Contracts.Entities.Group.CreateGroupRequest(
                    code,
                    instituteId
                    )
                );

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<GroupModel>(await response.Content.ReadAsStringAsync());
            else
                return null;
        }
    }
}
