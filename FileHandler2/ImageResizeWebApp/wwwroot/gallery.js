var options = {
    weekday: 'long', year: 'numeric', month: 'long', day: 'numeric',
    hour: '2-digit', minute: '2-digit', second: '2-digit'
};
new Vue({
    el: '#app',
    data: {
        files: [],
        search: '',
        image: '',
        fileToUpload: {}
    },
    computed: {
        filteredFiles() {
            const regex = new RegExp(this.search, 'gi');
            return this.files.filter(file => file.fileName.match(regex));
        }
    },
    created: function() {
        var vm = this;
        $.ajax({
          url: 'http://filehandler2hackathonapi.azurewebsites.net/api/file/list',
          type: 'POST',
          contentType: 'application/json; charset=utf-8',
          dataType: 'json',
          success: function(res) {
            res.files.forEach(f => {
                f.dateCreated = (new Date(f.dateCreated))
                        .toLocaleDateString('nb-NO', options)
            })
            vm.files = res.files;
          }
        })
    },
    methods: {
        uploadFile() {
            console.log('uploadFile')
            $.ajax({
                url: 'http://filehandler2hackathonapi.azurewebsites.net/api/file/post',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: {
                    file: this.fileToUpload
                },
                dataType: 'json',
                success: function (res) {
                    console.log(res)
                }
            })
        },
        onFileChange(e) {
          var files = e.target.files || e.dataTransfer.files;
          if (!files.length) return;
            this.fileToUpload = {
                "name": files[0].name,
                "content": "VGhpcyBpcyBhIHRlc3QgZmlsZSBlbmNvZGVkIHdpdGggQmFzZTY0",
                "userName": "slupski",
                "tags": [
                    "SomeTag", "SomeOtherTag"
                ],
                "size": files[0].size
            };
          console.log(files[0]);
          this.createImage(files[0]);
        },
        createImage(file) {
          var image = new Image();
          var reader = new FileReader();
          var vm = this;

          reader.onload = (e) => {
            vm.image = e.target.result;
          };
          reader.readAsDataURL(file);
        },
        removeImage: function (e) {
          this.image = '';
        }
      }
})