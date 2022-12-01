//category jtable
var cachedCategoryOptions = null;

$(document).ready(function () {
    $('#CategoryTableContainer').jtable({
        title: 'Categories',
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
                title: 'Name',
                width: '35%'
            },
            parentCategoryId: {
                title: 'Parent category',
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
                                debugger;
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
                title: 'Position',
                width: '20%'
            }
        }
    });
    $('#CategoryTableContainer').jtable('load')
});

//buildingPlan jtable
$(document).ready(function () {
    $('#BuildingPlanTableContainer').jtable({
        title: 'Building Plans',
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
                title: 'Name',
                width: '90%'
            }
        }
    });
    $('#BuildingPlanTableContainer').jtable('load')
});

//buildingType jtable
$(document).ready(function () {
    $('#BuildingTypeTableContainer').jtable({
        title: 'Building Types',
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
                title: 'Name',
                width: '90%'
            }
        }
    });
    $('#BuildingTypeTableContainer').jtable('load')
});

//estateCondition jtable
$(document).ready(function () {
    $('#EstateConditionTableContainer').jtable({
        title: 'Estate Conditions',
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
                title: 'Name',
                width: '90%'
            }
        }
    });
    $('#EstateConditionTableContainer').jtable('load')
});

//estateOption jtable
$(document).ready(function () {
    $('#EstateOptionTableContainer').jtable({
        title: 'Estate Options',
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
                title: 'Name',
                width: '90%'
            }
        }
    });
    $('#EstateOptionTableContainer').jtable('load')
});

//zone jtable
$(document).ready(function () {
    $('#ZoneTableContainer').jtable({
        title: 'Zones',
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
                title: 'Name',
                width: '90%'
            }
        }
    });
    $('#ZoneTableContainer').jtable('load')
});