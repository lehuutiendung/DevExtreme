<template>
    <div class="wrap-filter" :style="{display : filterPopupState}">
        <div class="wrap-header-filter">
            <h2 class="filter-text">{{ this.$resourceVn.FilterText }}</h2>
            <div class="icon-36 wrap-exit-filter" @click="exitFilterPopup()">
                <div class="icon-20 icon-filter"></div>
            </div>
        </div>
        <div class="wrap-content-filter">
            <InputSearch/>
            <div class="wrap-policy-name" :class="{'background-active' : clickedPolicyName}">
                 <div class="box-flex checkbox-policy-name" @click="clickPolicyName()">
                     <div class="icon-20 checkbox-default" :class="{'checkbox-checked' : clickedPolicyName}"></div>
                     <div class="name policy-name">{{ this.$resourceVn.PolicyName }}</div>
                 </div>
                 <ComboboxText v-if="clickedPolicyName" :dataCombobox="dataCombobox.DataComboboxPolicyName"/>
                 <Input v-if="clickedPolicyName"/>
            </div>
            <div class="wrap-policy-name" :class="{'background-active' : clickedPosition}">
                 <div class="box-flex checkbox-policy-name" @click="clickPosition()">
                     <div class="icon-20 checkbox-default" :class="{'checkbox-checked' : clickedPosition}"></div>
                     <div class="name policy-name">{{ this.$resourceVn.PositionApply }}</div>
                 </div>
                 <ComboboxText v-if="clickedPosition" :dataCombobox="dataCombobox.DataComboboxApply"/>
                 <ComboboxTag v-if="clickedPosition" :placeholder="this.$resourceVn.AllPositionApply"/>
            </div>
            <div class="wrap-policy-name" :class="{'background-active' : clickedEmployee}">
                 <div class="box-flex checkbox-policy-name" @click="clickEmployee()">
                     <div class="icon-20 checkbox-default" :class="{'checkbox-checked' : clickedEmployee}"></div>
                     <div class="name policy-name">{{ this.$resourceVn.EmployeeApply }}</div>
                 </div>
                 <ComboboxText v-if="clickedEmployee" :dataCombobox="dataCombobox.DataComboboxApply"/>
                 <ComboboxTag v-if="clickedEmployee" :placeholder="this.$resourceVn.AllEmployeeApply"/>
            </div>
        </div>
        <div class="wrap-footer-filter">
            <Button class="button-white" :buttonName="this.$resourceVn.ButtonFilterCancel" @click.native="exitFilterPopup()"/>
            <Button class="button-green" :buttonName="this.$resourceVn.ButtonApplyText"/>
        </div>
    </div>
</template>
<script>
import DataCombobox from "../../../js/fake-data/data-combobox"

import InputSearch from "../../base/input-search/BaseInputSearch.vue"
import ComboboxText from "../../base/combobox-text/BaseComboboxText.vue"
import ComboboxTag from "../../base/combobox/BaseCombobox.vue"
import Input from "../../base/input/BaseInput.vue"
import Button from "../../base/button/BaseButton.vue"
export default {
    name: "FilterBox",
    components:{ 
        InputSearch,
        ComboboxText,
        ComboboxTag,
        Input,
        Button
    },
    props:{
        filterShow:{
            type: Boolean,
            default(){
                return false;
            }
        }
    },
    data() {
        return {
            dataCombobox: DataCombobox,         //Dữ liệu đổ xuống của combobox
            clickedPolicyName: false,           //Biến check đã click checkbox "Tên chính sách"
            clickedPosition: false,             //Biến check đã click checkbox "Vị trí áp dụng"
            clickedEmployee: false,             //Biến check đã click checkbox "Nhân viên áp dụng"
        }
    },
    computed: {
        /**
         * @description Thay đổi trạng thái của popup bộ lọc
         * @date 18/09/2021
         * @createdBy LHTDung
         */
        filterPopupState() {
            if (this.filterShow) {
                return "flex";
            } else {
                return "none";
            }
        },

    },
    methods: {
        /**
         * @description Click button exit -> Đóng popup filter
         * @date 19/09/2021
         * @createdBy LHTDung
         */
        exitFilterPopup(){
            this.$emit('hideFilterPopup');
        },

        /**
         * @description Click checkbox "Tên chính sách"
         * @date 19/09/2021
         * @createdBy LHTDung
         */
        clickPolicyName(){
            this.clickedPolicyName = !this.clickedPolicyName;
        },

        /**
         * @description Click checkbox "Vị trí áp dụng"
         * @date 19/09/2021
         * @createdBy LHTDung
         */
        clickPosition(){
            this.clickedPosition = !this.clickedPosition;
        },

        /**
         * @description Click checkbox "Nhân viên áp dụng"
         * @date 19/09/2021
         * @createdBy LHTDung
         */
        clickEmployee(){
            this.clickedEmployee = !this.clickedEmployee;
        }
    },
}
</script>
<style scoped>
    @import "../../../css/base/filter/filter.css";
</style>