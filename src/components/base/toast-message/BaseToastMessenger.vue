<template>
    <div class="box-toast" :class="{'show': showToast}" ref="boxToast">
        <div class="toast background-success">
          <div class="wrap-start-toast">
            <div class="icon-24 icon-toast icon-success"></div>
            <div class="toast-content">
              <p class="toast-text">{{ contentToast }} thành công.</p>
            </div>
          </div>
        </div>
    </div>
</template>
<script>
import { EventBus } from '../../../main'
export default {
    name: "ToastMessenger",
    data() {
      return {
        typeToast: 0, //Loại Toast (0: Thành công,  )
        showToast: false, //Mặc định Toast bị ẩn
        contentToast: '', //Nội dung của Toast
      }
    },
    mounted() {
        EventBus.$on('showToast', (content, type) => {
            this.contentToast = content;
            this.typeToast = type;
            this.showToast = true;
            if(this.showToast){
              clearTimeout(timeOut);
            }
            var timeOut = setTimeout(() => {
              this.showToast = false;
            }, 2000);
        })
    },
}
</script>
<style scoped>
    @import "../../../css/base/toast-message/toastmessenger.css";
</style>