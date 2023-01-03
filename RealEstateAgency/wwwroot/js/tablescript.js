//category jtable
var cachedCategoryOptions = null;
var cachedZoneOptions = null;
var cultureinfo = document.getElementById('cultureinfo');
let scriptEle = document.createElement("script");
//Localization
var messagesEN = {
    serverCommunicationError: 'An error occured while communicating to the server.',
    loadingMessage: 'Loading records...',
    noDataAvailable: 'No data available!',
    addNewRecord: 'Add new record',
    editRecord: 'Edit Record',
    areYouSure: 'Are you sure?',
    deleteConfirmation: 'This record will be deleted. Are you sure?',
    save: 'Save',
    saving: 'Saving',
    cancel: 'Cancel',
    deleteText: 'Delete',
    deleting: 'Deleting',
    error: 'Error',
    close: 'Close',
    cannotLoadOptionsFor: 'Can not load options for field {0}',
    pagingInfo: 'Showing {0}-{1} of {2}',
    pageSizeChangeLabel: 'Row count',
    gotoPageLabel: 'Go to page',
    canNotDeletedRecords: 'Can not deleted {0} of {1} records!',
    deleteProggress: 'Deleted {0} of {1} records, processing...'
};
var messagesRO = {
    serverCommunicationError: 'Eroare la comunicarea cu serverul.',
    loadingMessage: 'Încărcarea înregistrărilor...',
    noDataAvailable: 'Nu există înregistrări!',
    addNewRecord: 'Adaugă',
    editRecord: 'Editare',
    areYouSure: 'Sunteți sigur?',
    deleteConfirmation: 'Înregistrarea va fi ștearsă. Continuați?',
    save: 'Salvează',
    saving: 'Salvare în curs...',
    cancel: 'Anulează',
    deleteText: 'Șterge',
    deleting: 'Ștergere în curs...',
    error: 'Eroare',
    close: 'Închide',
    cannotLoadOptionsFor: 'Imposibil de încărcat datele câmpului {0}',
    pagingInfo: 'Înregistrarile {0} - {1} din {2}',
    canNotDeletedRecords: 'Imposibil de șters {0} din {1} înregistrări!',
    deleteProggress: 'Ștergere: {0} din {1} înregistrări, în curs de execuție...',
    pageSizeChangeLabel: 'Număr de înregistrări',
    gotoPageLabel: 'Mergi la pagină'
};
var messagesRU = {
    serverCommunicationError: 'Ошибка связи с сервером.',
    loadingMessage: 'Загрузка...',
    noDataAvailable: 'Данные отсутствуют',
    addNewRecord: 'Добавить',
    editRecord: 'Изменить',
    areYouSure: 'Вы уверены?',
    deleteConfirmation: 'Удалить запись?',
    save: 'Сохранить',
    saving: 'Сохранение...',
    cancel: 'Отмена',
    deleteText: 'Удалить',
    deleting: 'Удаление...',
    error: 'Ошибка',
    close: 'Закрыть',
    cannotLoadOptionsFor: 'Невозможно загрузить варианты для поля {0}',
    pagingInfo: 'Записи с {0} по {1} из {2}',
    canNotDeletedRecords: 'Невозможно удалить записи: {0} из {1}!',
    deleteProggress: 'Удаление {0} из {1} записей...',
    pageSizeChangeLabel: 'Строк',
    gotoPageLabel: 'На страницу'
};
var localizedMessages = [];
var categoryTitle;
var categoryTable;
var parentCategoryTitle;
var positionTitle;
var buildingPlanTitle;
var buildingPlanTable;
var buildingTypeTitle;
var buildingTypeTable;
var estateConditionTitle;
var estateConditionTable;
var estateOptionTitle;
var estateOptionTable;
var zoneTitle;
var parentZoneTitle;
var zoneTable;

$(document).ready(function () {
    switch (cultureinfo.value) {
        case 'ro-RO':
            localizedMessages = messagesRO;
            categoryTitle = 'Nume';
            categoryTable = 'Categorii';
            parentCategoryTitle = 'Categoria Părinte';
            positionTitle = 'Poziția';
            buildingPlanTitle = 'Nume';
            buildingPlanTable = 'Planuri';
            buildingTypeTitle = 'Nume';
            buildingTypeTable = 'Tipuri';
            estateConditionTitle = 'Nume';
            estateConditionTable = 'Condiții';
            estateOptionTitle = 'Nume';
            estateOptionTable = 'Opțiuni';
            zoneTitle = 'Nume';
            zoneTable = 'Zone';
            parentZoneTitle = 'Zona părinte';

            scriptEle.setAttribute("src", '/js/jquery.validationEngine-ro.js');
            scriptEle.setAttribute("type", "text/javascript");
            scriptEle.setAttribute("async", true);
            document.body.appendChild(scriptEle);
            break;
        case 'ru-RU':
            localizedMessages = messagesRU;
            categoryTitle = 'Название';
            categoryTable = 'Категории';
            parentCategoryTitle = 'Базовая категория';
            positionTitle = 'Позиция';
            buildingPlanTitle = 'Название';
            buildingPlanTable = 'Планы';
            buildingTypeTitle = 'Название';
            buildingTypeTable = 'Типы';
            estateConditionTitle = 'Название';
            estateConditionTable = 'Условия';
            estateOptionTitle = 'Название';
            estateOptionTable = 'Параметры';
            zoneTitle = 'Название';
            zoneTable = 'Области';
            parentZoneTitle = 'Базовая область';

            scriptEle.setAttribute("src", '/js/jquery.validationEngine-ru.js');
            scriptEle.setAttribute("type", "text/javascript");
            scriptEle.setAttribute("async", true);
            document.body.appendChild(scriptEle);
            break;
        default:
            localizedMessages = messagesEN;
            categoryTitle = 'Name';
            categoryTable = 'Categories';
            parentCategoryTitle = 'Parent Category';
            positionTitle = 'Position';
            buildingPlanTitle = 'Name';
            buildingPlanTable = 'Building Plans';
            buildingTypeTitle = 'Name';
            buildingTypeTable = 'Building Types';
            estateConditionTitle = 'Name';
            estateConditionTable = 'Estate Conditions';
            estateOptionTitle = 'Name';
            estateOptionTable = 'Estate Options';
            zoneTitle = 'Name';
            zoneTable = 'Zones';
            parentZoneTitle = 'Parent zone';

            scriptEle.setAttribute("src", '/js/jquery.validationEngine-en.js');
            scriptEle.setAttribute("type", "text/javascript");
            scriptEle.setAttribute("async", true);
            document.body.appendChild(scriptEle);
            break;
    }
});

$(document).ready(function () {
    $('#CategoryTableContainer').jtable({
        messages: localizedMessages,
        title: categoryTable,
        actions: {
            listAction: '/api/Categories/GetCategories',
            createAction: '/api/Categories/PostCategory',
            updateAction: '/api/Categories/PutCategory',
            deleteAction: '/api/Categories/DeleteCategory'
        },
        fields: {
            id: {
                key: true,
                list: false
            },
            categoryName: {
                title: categoryTitle,
                width: '35%',
                inputClass: 'validate[required]'
            },
            parentCategoryId: {
                title: parentCategoryTitle,
                width: '35%',
                options: function (data) {

                    if (!cachedCategoryOptions) {
                        //debugger;
                        var options = [];

                        $.ajax({ //Not found in cache, get from server
                            url: '/api/Categories/GetCategories',
                            type: 'POST',
                            dataType: 'json',
                            async: false,
                            success: function (data_success) {
                                if (data_success.result != 'OK') {
                                    alert(data_success.message);
                                    return;
                                }
                                options = data_success.records.map((category) => ({ DisplayText: category.categoryName, Value: category.id }));
                                options.push({ DisplayText: "", Value: "" });
                            }
                        });
                        return cachedCategoryOptions = options;
                    }
                    else {
                        if (data.source == "edit") {
                            return cachedCategoryOptions.filter(category => category.Value != data.record.id);
                        }
                        else {
                            return cachedCategoryOptions;
                        }
                    }
                }
            },
            position: {
                title: positionTitle,
                width: '20%',
                inputClass: 'validate[required]'
            }
        },
        //Initialize validation logic when a form is created
        formCreated: function (event, data) {
            data.form.validationEngine();
        },
        //Validate form when it is being submitted
        formSubmitting: function (event, data) {
            return data.form.validationEngine('validate');
        },
        //Dispose validation logic when form is closed
        formClosed: function (event, data) {
            data.form.validationEngine('hide');
            data.form.validationEngine('detach');
        }
    });
    $('#CategoryTableContainer').jtable('load')
});

//buildingPlan jtable
$(document).ready(function () {
    $('#BuildingPlanTableContainer').jtable({
        messages: localizedMessages,
        title: buildingPlanTable,
        actions: {
            listAction: '/api/BuildingPlans/GetBuildingPlans',
            createAction: '/api/BuildingPlans/PostBuildingPlan',
            updateAction: '/api/BuildingPlans/PutBuildingPlan',
            deleteAction: '/api/BuildingPlans/DeleteBuildingPlan'
        },
        fields: {
            id: {
                key: true,
                list: false
            },
            buildingPlanName: {
                title: buildingPlanTitle,
                width: '90%',
                inputClass: 'validate[required]'
            }
        },
        //Initialize validation logic when a form is created
        formCreated: function (event, data) {
            data.form.validationEngine();
        },
        //Validate form when it is being submitted
        formSubmitting: function (event, data) {
            return data.form.validationEngine('validate');
        },
        //Dispose validation logic when form is closed
        formClosed: function (event, data) {
            data.form.validationEngine('hide');
            data.form.validationEngine('detach');
        }
    });
    $('#BuildingPlanTableContainer').jtable('load')
});

//buildingType jtable
$(document).ready(function () {
    $('#BuildingTypeTableContainer').jtable({
        messages: localizedMessages,
        title: buildingTypeTable,
        actions: {
            listAction: '/api/BuildingTypes/GetBuildingTypes',
            createAction: '/api/BuildingTypes/PostBuildingType',
            updateAction: '/api/BuildingTypes/PutBuildingType',
            deleteAction: '/api/BuildingTypes/DeleteBuildingType'
        },
        fields: {
            id: {
                key: true,
                list: false
            },
            buildingTypeName: {
                title: buildingTypeTitle,
                width: '90%',
                inputClass: 'validate[required]'
            }
        },
        //Initialize validation logic when a form is created
        formCreated: function (event, data) {
            data.form.validationEngine();
        },
        //Validate form when it is being submitted
        formSubmitting: function (event, data) {
            return data.form.validationEngine('validate');
        },
        //Dispose validation logic when form is closed
        formClosed: function (event, data) {
            data.form.validationEngine('hide');
            data.form.validationEngine('detach');
        }
    });
    $('#BuildingTypeTableContainer').jtable('load')
});

//estateCondition jtable
$(document).ready(function () {
    $('#EstateConditionTableContainer').jtable({
        messages: localizedMessages,
        title: estateConditionTable,
        actions: {
            listAction: '/api/EstateConditions/GetEstateConditions',
            createAction: '/api/EstateConditions/PostEstateCondition',
            updateAction: '/api/EstateConditions/PutEstateCondition',
            deleteAction: '/api/EstateConditions/DeleteEstateCondition'
        },
        fields: {
            id: {
                key: true,
                list: false
            },
            estateConditionName: {
                title: estateConditionTitle,
                width: '90%',
                inputClass: 'validate[required]'
            }
        },
        //Initialize validation logic when a form is created
        formCreated: function (event, data) {
            data.form.validationEngine();
        },
        //Validate form when it is being submitted
        formSubmitting: function (event, data) {
            return data.form.validationEngine('validate');
        },
        //Dispose validation logic when form is closed
        formClosed: function (event, data) {
            data.form.validationEngine('hide');
            data.form.validationEngine('detach');
        }
    });
    $('#EstateConditionTableContainer').jtable('load')
});

//estateOption jtable
$(document).ready(function () {
    $('#EstateOptionTableContainer').jtable({
        messages: localizedMessages,
        title: estateOptionTable,
        actions: {
            listAction: '/api/EstateOptions/GetEstateOptions',
            createAction: '/api/EstateOptions/PostEstateOption',
            updateAction: '/api/EstateOptions/PutEstateOption',
            deleteAction: '/api/EstateOptions/DeleteEstateOption'
        },
        fields: {
            id: {
                key: true,
                list: false
            },
            estateOptionName: {
                title: estateOptionTitle,
                width: '90%',
                inputClass: 'validate[required]'
            }
        },
        //Initialize validation logic when a form is created
        formCreated: function (event, data) {
            data.form.validationEngine();
        },
        //Validate form when it is being submitted
        formSubmitting: function (event, data) {
            return data.form.validationEngine('validate');
        },
        //Dispose validation logic when form is closed
        formClosed: function (event, data) {
            data.form.validationEngine('hide');
            data.form.validationEngine('detach');
        }
    });
    $('#EstateOptionTableContainer').jtable('load')
});

//zone jtable
$(document).ready(function () {
    $('#ZoneTableContainer').jtable({
        messages: localizedMessages,
        title: zoneTable,
        actions: {
            listAction: '/api/Zones/GetZones',
            createAction: '/api/Zones/PostZone',
            updateAction: '/api/Zones/PutZone',
            deleteAction: '/api/Zones/DeleteZone'
        },
        fields: {
            id: {
                key: true,
                list: false
            },
            zoneName: {
                title: zoneTitle,
                width: '45%',
                inputClass: 'validate[required]'
            },
            parentZoneId: {
                title: parentZoneTitle,
                width: '45%',
                options: function (data) {

                    if (!cachedZoneOptions) {
                        //debugger;
                        var options = [];

                        $.ajax({ //Not found in cache, get from server
                            url: '/api/Zones/GetZones',
                            type: 'POST',
                            dataType: 'json',
                            async: false,
                            success: function (data_success) {
                                if (data_success.result != 'OK') {
                                    alert(data_success.message);
                                    return;
                                }
                                options = data_success.records.map((zone) => ({ DisplayText: zone.zoneName, Value: zone.id }));
                                options.push({ DisplayText: "", Value: "" });
                            }
                        });
                        return cachedZoneOptions = options;
                    }
                    else {
                        if (data.source == "edit") {
                            return cachedZoneOptions.filter(zone => zone.Value != data.record.id);
                        }
                        else {
                            return cachedZoneOptions;
                        }
                    }
                }
            }
        },
        //Initialize validation logic when a form is created
        formCreated: function (event, data) {
            data.form.validationEngine();
        },
        //Validate form when it is being submitted
        formSubmitting: function (event, data) {
            return data.form.validationEngine('validate');
        },
        //Dispose validation logic when form is closed
        formClosed: function (event, data) {
            data.form.validationEngine('hide');
            data.form.validationEngine('detach');
        }
    });
    $('#ZoneTableContainer').jtable('load')
});