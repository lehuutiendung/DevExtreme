import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import Vue from 'vue'
import App from './App.vue'
import ResourceVn from '../src/resources/resource-vn.js'
export const EventBus = new Vue();

Vue.config.productionTip = false

Vue.prototype.$resourceVn = ResourceVn;

new Vue({
  render: h => h(App),
}).$mount('#app')
