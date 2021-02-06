
var formScript = {
    formValidation: {
        validate: function () {
            $('.input-validation-error').parents('.form-group').addClass('has-error');
            $('.textarea-validation-error').parents('.form-group').addClass('has-error');
            $('.field-validation-error').addClass('text-danger');
            $('.field-validation-error').show();
        }
    }
}