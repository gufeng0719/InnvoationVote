var vm = new Vue({
    el: '#app',
    data: {
        imgBaseUrl: '/Images/Into/',
        project: {
            
        }//项目对象
    },
    methods: {
        initData: function () {
            //加载本人今天已经投票ID
            var that = this;
            var pid = getRequest().pid;
            ajax('/Vote/GetProject',
                {pid:pid},
                function (data) {
                    that.project = data.data[0];
                });
        }
    },
    mounted: function () {
        this.initData();
    }
})