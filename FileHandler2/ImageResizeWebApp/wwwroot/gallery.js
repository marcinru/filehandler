var options = {
    weekday: 'long', year: 'numeric', month: 'long', day: 'numeric',
    hour: '2-digit', minute: '2-digit', second: '2-digit'
},
vm = new Vue({
    el: '#app',
    data: {
        files: [],
        search: '',
        image: '',
        fileToUpload: {},
        currentImg: '',
        modalTitle: ''
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
                var date = new Date(f.dateCreated);
                date.setTime(date.getTime() + 2*3600*1000);
                f.dateCreated = date.toLocaleDateString('nb-NO', options)
            })
            vm.files = res.files;
          }
        })
    },
    methods: {
        uploadFile() {
            $.ajax({
                url: 'http://filehandler2hackathonapi.azurewebsites.net/api/file/post',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(this.fileToUpload),
                cache: false,
            }).done(function (res) {
                vm.removeImage()
            })
        },
        getFile(uid) {
            console.log('getFile')
            var fileUid = '6d2b9487-9b0f-470f-a381-3276cb296e87';
            $.get('https://filehandler2hackathonapi.azurewebsites.net/api/file/Download?fileUid=' + uid,
                function(res) {
                    vm.modalTitle = res.fileName;
                    var indexLast = res.fileName.lastIndexOf('.')
                    var extension = res.fileName.substring(indexLast + 1);
                    vm.currentImg = 'data:image/' + extension + ';base64,' + res.content;
                })
        },
        onFileChange(e) {
          var files = e.target.files || e.dataTransfer.files;
            if (!files.length) return;
            getBase64(files[0]).then(
              base64content => {
                var commaIndex = base64content.indexOf(',') + 1;
                vm.fileToUpload.content = base64content.substring(commaIndex)
              }
            );
            this.fileToUpload = {
                "name": files[0].name,
                "content": '',
                "userName": "slupski",
                "tags": [
                    "SomeTag", "SomeOtherTag"
                ],
                "size": files[0].size
            };
          this.createImage(files[0]);
        },
        createImage(file) {
          var image = new Image();
          var reader = new FileReader();

          reader.onload = (e) => {
            vm.image = e.target.result;
          };
          reader.readAsDataURL(file);
        },
        removeImage: function (e) {
          this.image = '';
          this.fileToUpload = {};
        }
      }
    })
function getBase64(file) {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
  });
}