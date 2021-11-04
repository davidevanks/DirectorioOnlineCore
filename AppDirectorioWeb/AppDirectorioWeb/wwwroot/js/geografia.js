function Geografia() {
    this.CargarProvincias = function (pickerId) {
        let urlProvincia = "Provincia";
        let valueProperty = "PkProvincia";
        let textProperty = "Nombre";

        $backend.GetToBackend(urlProvincia)
            .then(data => {
                console.log(data);
                for (var i = 0; i < data.length; i++) {
                    var opt = data[i];
                    $(pickerId).append(new Option(opt[textProperty], opt[valueProperty]));
                }
                $(pickerId).selectpicker('refresh');
            });
    }

    this.CargarProvinciasSelected = function (pickerId,valueSelected) {
        let urlProvincia = "Provincia";
        let valueProperty = "PkProvincia";
        let textProperty = "Nombre";

        console.log("Seleccionar: " + valueSelected);

        $backend.GetToBackend(urlProvincia)
            .then(data => {
                console.log(data);
                for (var i = 0; i < data.length; i++) {
                    var opt = data[i];
                    $(pickerId).append(new Option(opt[textProperty], opt[valueProperty]));
                }
                
                $(pickerId).selectpicker('refresh');
                $(pickerId).selectpicker('val', valueSelected);
            });
    }

    this.CambioProvincia = function (provinciaPicker, cantonPicker, distritoPicker) {
        LimparSelectPicker(distritoPicker);
        LimparSelectPicker(cantonPicker);

        let provinciaId = $(provinciaPicker).val();
        console.info("Provincia Id:" + provinciaId);

        if (provinciaId != 0) {
            $(cantonPicker).prop("disabled", false);
            this.CargarCantones(provinciaId, cantonPicker);
        }
    }

    this.CargarCantones = function (provinciaId, pickerId) {
        let urlcanton = "Canton/ListarPorProvincia/" + provinciaId;
        let valueProperty = "PkCanton";
        let textProperty = "Nombre";

        $backend.GetToBackend(urlcanton)
            .then(data => {
                console.log(data);
                for (var i = 0; i < data.length; i++) {
                    var opt = data[i];
                    $(pickerId).append(new Option(opt[textProperty], opt[valueProperty]));
                }
                $(pickerId).selectpicker('refresh');
            });
    }

    this.CargarCantonesSelected = function (provinciaId, pickerId, valueSelected) {
        let urlcanton = "Canton/ListarPorProvincia/" + provinciaId;
        let valueProperty = "PkCanton";
        let textProperty = "Nombre";

        console.log("Seleccionar: " + valueSelected);

        $backend.GetToBackend(urlcanton)
            .then(data => {
                console.log(data);
                for (var i = 0; i < data.length; i++) {
                    var opt = data[i];
                    $(pickerId).append(new Option(opt[textProperty], opt[valueProperty]));
                }
                $(pickerId).selectpicker('refresh');
                $(pickerId).selectpicker('val', valueSelected);
            });
    }

    this.CambioCanton = function (cantonPicker, distritoPicker) {
        LimparSelectPicker(distritoPicker);

        let cantonId = $(cantonPicker).val();
        console.info("Canton Id:" + cantonId);

        if (cantonId != 0) {
            $(distritoPicker).prop("disabled", false);
            this.CargarDistritos(cantonId, distritoPicker);
        }
    }

    this.CargarDistritos = function (cantonId, pickerId) {
        let urlDistrito = "Distrito/ListarPorCanton/" + cantonId;
        let valueProperty = "PkDistrito";
        let textProperty = "Nombre";

        $backend.GetToBackend(urlDistrito)
            .then(data => {
                console.log(data);
                for (var i = 0; i < data.length; i++) {
                    var opt = data[i];
                    $(pickerId).append(new Option(opt[textProperty], opt[valueProperty]));
                }
                $(pickerId).selectpicker('refresh');
            });
    }

    this.CargarDistritosSelected = function (cantonId, pickerId, valueSelected) {
        let urlDistrito = "Distrito/ListarPorCanton/" + cantonId;
        let valueProperty = "PkDistrito";
        let textProperty = "Nombre";

        console.log("Seleccionar: " + valueSelected);

        $backend.GetToBackend(urlDistrito)
            .then(data => {
                console.log(data);
                for (var i = 0; i < data.length; i++) {
                    var opt = data[i];
                    $(pickerId).append(new Option(opt[textProperty], opt[valueProperty]));
                }
                $(pickerId).selectpicker('refresh');
                $(pickerId).selectpicker('val', valueSelected);
            });
    }
}

const $geografia = new Geografia();

function LimparSelectPicker(PickerId) {
    $(PickerId).find('option').not(':first').remove();
    $(PickerId).prop("disabled", true);
    $(PickerId).selectpicker('refresh');
}