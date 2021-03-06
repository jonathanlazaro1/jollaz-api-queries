namespace JollazApiQueries.Model.Core.Errors
{
    public class ErrorMessages_por : IErrorMessages
    {
        public string NoOfOperatorsXNoOfFilters => "O número de operadores lógicos é incompatível com o número de filtros.";

        public string ParameterCastError => "Não foi possível converter o valor do parâmetro para o tipo de dados da propriedade";

        public string InvalidCriterion => "Critério não suportado para esta propriedade";

        public string PropertyNotFound => "Propriedade não encontrada";

        public string NoNestedPropsInCollections => "Filtragem em propriedades aninhadas de coleções de classes não é suportada.";

        public string LogicalOperatorOutOfRange => "Não foi possível inferir o operador lógico fornecido.";

        public string PropertyTypeNotSupported => "Tipo de dados da propriedade não é suportado";

        public string OnlySupportedInAdvancedFilter => "Esta operação somente é suportada ao se utilizar um filtro avançado.";

        public string SearchParameterIsNull => "Parâmetro de pesquisa não pode ser nulo";

        public string UnableToSelect => "Não foi possível selecionar: alguma das propriedades fornecidas não foi encontrada.";

        public string UnableToGroup => "Não foi possível agrupar: a instrução fornecida é inválida.";

        public string UnrecognizedProcessingMethod => "Método de processamento inválido.";
    }
}