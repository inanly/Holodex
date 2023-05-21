new Vue({
    el: '#app',
    vuetify: new Vuetify(),
    data: () => ({
        drawer: false
    }),
    methods: {
        scrollTo(section) {
            var element = document.getElementById(section);
            element.scrollIntoView({ behavior: "smooth" });
        }
    }
});