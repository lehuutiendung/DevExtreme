<template>
    <div class="wrap-loading" :class="{'show-loading' : showLoading}">
        <div class="lds-ring"><div></div><div></div><div></div><div></div></div>
    </div>
</template>
<script>
import {EventBus} from "../../../main"
export default {
    name: 'Loading',
    props:{
        showLoading: {
            type: Boolean,
            default(){
                return false;
            }
        }
    },
    mounted() {
        EventBus.$on('showLoading', (value) => {
            this.showLoading = value;
        })
    },
}
</script>
<style scoped>
    @import "../../../css/base/loading/base-loading.css";
</style>