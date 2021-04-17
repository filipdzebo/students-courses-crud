import Vue from 'vue'
import App from './App.vue'
import router from './router'
import axios from 'axios'
import VueAxios from 'vue-axios'
import vuetify from './plugins/vuetify';
import {ValidationProvider} from 'vee-validate/dist/vee-validate.full.esm';
import {ValidationObserver} from 'vee-validate'
import VueSweetalert2 from 'vue-sweetalert2'
import 'sweetalert2/dist/sweetalert2.min.css';

Vue.component('ValidationProvider',ValidationProvider);
Vue.component('ValidationObserver',ValidationObserver);

Vue.use(VueAxios, axios)
Vue.use(VueSweetalert2)

Vue.prototype.$axios=axios.create({
  baseURL:"https://localhost:44319/"
})

Vue.prototype.$token="";
Vue.prototype.$logged=false;

Vue.config.productionTip = false

new Vue({
  router,
  vuetify,
  render: h => h(App)
}).$mount('#app')
