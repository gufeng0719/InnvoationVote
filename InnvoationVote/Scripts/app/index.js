var vm = new Vue({
    el: '#app',
    data: {
        where: {
            ProjectId: '',
            ProjectName: '',
            TypeId: ''
        },
        order: {
            VoteNumber:'0'
        },
        list: [],
        loading: false,
        currentPage: 1,
        total: 0
    },
    methods: {
        getpage: function (page) {
            var that = this;
            that.loading = true;
            that.where.CurrentPage = page;
            ajax('/Home/GetProjectVotePage',
                {
                    para: that.where,
                    order: that.order
                },
                function(data) {
                    that.list = data.data;
                    that.total = data.total;
                    that.loading = false;
                });
        }
        //,
        //selectDetail: function (userId) {
        //    window.location.href = "/CsOrder?userId=" + userId
        //}
    },
    mounted: function () {
        this.getpage(this.currentPage);
    }
})