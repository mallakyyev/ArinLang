function InitTinyMce(selector, imageUploadUrl) {
    tinymce.init({
        selector: selector, // change this value according to your HTML
        language: 'ru',
        cleanup_on_startup: false,
        trim_span_elements: false,
        verify_html: false,
        cleanup: false,

        valid_elements: '*[*]',
        height: "480",
        relative_urls: false,
        remove_script_host: true,
        convert_urls: true,
        images_upload_url: imageUploadUrl,
        image_advtab: true,
        plugins: 'print preview code searchreplace autolink directionality visualblocks visualchars image link media template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists textcolor wordcount  imagetools  contextmenu colorpicker textpattern help',
        toolbar1: 'formatselect | bold italic strikethrough forecolor backcolor | link | alignleft aligncenter alignright alignjustify  | numlist bullist outdent indent  | removeformat',
        file_browser_callback_types: 'file image media',
        toolbar: "forecolor backcolor toc charmap",
        templates: [
            { title: 'Standart content', content: '<div class="container"><div class="row"></div></div>' }
        ],
        file_picker_types: 'file image media'
    });
}