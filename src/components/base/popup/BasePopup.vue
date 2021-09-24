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
        <div class="popup-content" v-if="typePopup == this.$resourceVn.POPUP_EXIT_EDIT_TYPE">{{ this.$resourceVn.POPUP_EXIT_EDIT }}</div>
        <div class="popup-content" v-if="typePopup == this.$resourceVn.POPUP_ADD_TYPE">{{ this.$resourceVn.POPUP_ADD }}</div>
        <div class="popup-content" v-if="typePopup == this.$resourceVn.POPUP_DELETE_TYPE">{{ this.$resourceVn.POPUP_DELETE }} <span class="content">{{ content }}</span> {{ ' không?' }}</div>
        <div class="popup-content" v-if="typePopup == this.$resourceVn.POPUP_DELETE_MULTI_TYPE">{{ this.$resourceVn.POPUP_DELETE_MULTIPLE }}</div>
        <div class="popup-content" v-if="typePopup == this.$resourceVn.POPUP_STOPAPPLY_TYPE"> Bạn có chắc chắn muốn chuyển trạng thái chính sách lương <span class="content">{{ content.PolicyName }}</span> sang ngừng áp dụng không?</div>
        <div class="popup-content" v-if="typePopup == this.$resourceVn.POPUP_ACTIVEAPPLY_TYPE"> Bạn có chắc chắn muốn chuyển trạng thái chính sách lương <span class="content">{{ content.PolicyName }}</span> sang đang áp dụng không?</div>
        <div class="popup-content" v-if="typePopup == this.$resourceVn.POPUP_STOPAPPLY_MULTI_TYPE">{{ this.$resourceVn.POPUP_STOPAPPLY_MULTIPLE }}</div>

        <div class="popup-button" v-if="typePopup == this.$resourceVn.POPUP_EXIT_EDIT_TYPE">
            <Button class="button-white" :buttonName="this.$resourceVn.ButtonCancelText_2" width="80px" @click.native="clickCancel()"/>
            <Button class="button-white" :buttonName="this.$resourceVn.ButtonDontSaveText" @click.native="clickDontSave()"/>
            <Button class="button-green" :buttonName="this.$resourceVn.ButtonSaveText" width="80px"/>
        </div>
        <div class="popup-button" v-if="typePopup == this.$resourceVn.POPUP_ADD_TYPE">
            <Button class="button-green" :buttonName="this.$resourceVn.CLOSE" width="80px" @click.native="clickCancel()"/>
        </div>
        <div class="popup-button" v-if="typePopup == this.$resourceVn.POPUP_DELETE_TYPE || typePopup == this.$resourceVn.POPUP_DELETE_MULTI_TYPE">
            <Button class="button-white" :buttonName="this.$resourceVn.ButtonCancelText_2" width="80px" @click.native="clickCancel()"/>
            <Button class="button-red" :buttonName="this.$resourceVn.DeleteText" width="80px" @click.native="clickDelete()"/>
        </div>
        <div class="popup-button" v-if="typePopup == this.$resourceVn.POPUP_STOPAPPLY_TYPE || typePopup == this.$resourceVn.POPUP_ACTIVEAPPLY_TYPE || typePopup == this.$resourceVn.POPUP_STOPAPPLY_MULTI_TYPE">
            <Button class="button-white" :buttonName="this.$resourceVn.ButtonCancelText_2" width="80px" @click.native="clickCancel()"/>
            <Button class="button-green" :buttonName="this.$resourceVn.ButtonAgreeText" width="80px" @click.native="clickAgree()"/>
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
            showPopup: false,  //Mặc định ẩn popup
            typePopup: 0,      //Loại popup
            content: '',       //Nội dung popup
        }
    },
    mounted() {
        /**
         * Show popup cảnh báo rời màn chỉnh sửa
         * @created LHTDung
         * @date 23/09/2021
         */
        EventBus.$on('showPopupCancelEditForm', (value) => {
            this.showPopup = true;
            this.typePopup = value;
        })

        /**
         * Show popup cảnh báo bắt buộc phải có thành phần lương
         * @created LHTDung
         * @date 23/09/2021
         */
        EventBus.$on('showPopupAddForm', (value) => {
            this.showPopup = true;
            this.typePopup = value;
        }),

        /**
         * Show popup cảnh báo xóa 1 chính sách lương
         * @created LHTDung
         * @date 24/09/2021
         */
        EventBus.$on('showPopupDeleteSingle', (content, value) => {
            this.showPopup = true;
            this.content = content;
            this.typePopup = value;
        }),

        /**
         * Show popup cảnh báo xóa nhiều chính sách lương
         * @created LHTDung
         * @date 24/09/2021
         */
        EventBus.$on('showPopupDeleteMultiple', (value) => {
            this.showPopup = true;
            this.typePopup = value;
        }),

        /**
         * Show popup cảnh báo chuyển sang ngừng áp dụng chính sách
         * @created LHTDung
         * @date 24/09/2021
         */
        EventBus.$on('showPopupStopApply', (content, value) => {
            this.showPopup = true;
            this.typePopup = value;
            this.content = content;
        }),

        /**
         * Show popup cảnh báo chuyển sang đang áp dụng chính sách
         * @created LHTDung
         * @date 24/09/2021
         */
        EventBus.$on('showPopupActiveApply', (content, value) => {
            this.showPopup = true;
            this.typePopup = value;
            this.content = content;
        }),

        /**
         * Show popup cảnh báo ngừng áp dụng nhiều chính sách
         * @created LHTDung
         * @date 24/09/2021
         */
        EventBus.$on('showPopupStopApplyMultiple', (value) => {
            this.showPopup = true;
            this.typePopup = value;
        })
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
        },

        /**
         * @description Click button "Xóa" 
         * @date 24/9/2021
         * @createdBy LHTDung
         */
        clickDelete(){
            this.showPopup = false;
            this.$emit('acceptDelete');
        },

        /**
         * @description Xử lý sự kiện click button "Đồng ý" trong Popup chuyển trạng thái của chính sách lương
         * @date 24/09/2021
         * @created LHTDung
         */
        clickAgree(){
            this.showPopup = false;
            if(this.typePopup == this.$resourceVn.POPUP_STOPAPPLY_TYPE || this.typePopup == this.$resourceVn.POPUP_ACTIVEAPPLY_TYPE){
                this.$emit('changeStatusSingle', this.content);
            }
            if(this.typePopup == this.$resourceVn.POPUP_STOPAPPLY_MULTI_TYPE){
                this.$emit('changeStatusMulti');
            }
        }
    },
}
</script>
<style scoped>
    @import "../../../css/base/popup/base-popup.css";
</style>