namespace Common.Gen
{
    public abstract class HelperSysObjectsBaseBack : HelperSysObjectsBase
    {
        public abstract void DefineTemplateByTableInfoFieldsFront(Context config, TableInfo tableInfo, UniqueListInfo infos);
        public abstract void DefineTemplateByTableInfoFront(Context config, TableInfo tableInfo);
        public abstract void DefineTemplateByTableInfoFieldsBack(Context config, TableInfo tableInfo, UniqueListInfo infos);
        public abstract void DefineTemplateByTableInfoBack(Context config, TableInfo tableInfo);

    }
}
