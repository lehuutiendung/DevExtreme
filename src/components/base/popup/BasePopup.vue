<template>
    <!-- Popup cảnh báo hủy form thêm mới-->
  <div class="background-popup" :class="{'popup-active' : showPopup}">
    <div class="popup">
        <div class="popup-title">
            <div class="title">{{ this.$resourceVn.Notificate }}</div>
            <div class="icon-36 wrap-icon" @click="clickDontSave()">
                <div class="icon-20 icon-exit"></div>
            </div>
        </div>
        <div class="popup-content">{{ this.$resourceVn.PopupExitEdit }}</div>
        <div class="popup-button">
            <Button class="button-white" :buttonName="this.$resourceVn.ButtonCancelText_2" width="80px" @click.native="clickCancel()"/>
            <Button class="button-white" :buttonName="this.$resourceVn.ButtonDontSaveText" @click.native="clickDontSave()"/>
            <Button class="button-green" :buttonName="this.$resourceVn.ButtonSaveText" width="80px"/>
        </div>
    </div>
  </div>
</template>
<script> 
import { EventBus } from "../../../main"
import Button from "../button/BaseButton.vue"
export default {
    name: 'Popup',
    components:{
        Button,
    },
    data() {
        return {
            showPopup: false, //Mặc định ẩn popup
            
        }
    },
    methods: {
        /**
         * @description Click button "Hủy" Popup
         * @date 21/9/2021
         * @createdBy LHTDung
         */
        clickCancel(){
            this.showPopup = false;
        },

        /**
         * @description Click button "Không lưu" Popup
         * @date 21/9/2021
         * @createdBy LHTDung
         */
        clickDontSave(){
            this.showPopup = false;
            this.$emit('moveToMainScreen');
        }
    },
    mounted() {
        EventBus.$on('showPopupCancelEditForm', () => {
            this.showPopup = true;
        })
    },
}
</script>
<style scoped>
    @import "../../../css/base/popup/base-popup.css";
</style>