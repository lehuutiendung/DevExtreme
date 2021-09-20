<template>
    <div class="wrap-modal-box" :style="{ display: modalBoxState }">
        <div class="box-info">
            <div class="title-box div-flex">
                <div class="title-wrap">
                    <div class="title">{{ this.$resourceVn.ChooseComponent }}</div>
                </div>
                <div class="menu-wrap">
                    <div class="icon-36 flex-icon" @click="clickCancel()">
                        <div class="icon-20 exit"></div>
                    </div>
                </div>
            </div>
            <div class="wrap-content">
                <div class="wrap-tool-grid">
                    <InputSearch/>
                    <div class="space"></div>
                    <Dropdown width="230px" :listData="this.$resourceVn.Status" :typeDropdown="this.$resourceVn.DropdownStatus"/>
                </div>
                <div class="wrap-content-grid">
                    <DataGrid 
                        :listHeader="listHeader" 
                        :dataSource="dataSource" 
                        :modalBoxShow="modalBoxShow"
                    >
                        <template #Status="{ data }">
                            <div>{{ data.Status }}</div>
                        </template>
                    </DataGrid>
                </div>
                <TheFooter class="footer-custome"/>
                <div class="space-footer-button"></div>
            </div>
            <div class="flex-wrap-button">
                <Button class="button-white" :buttonName="this.$resourceVn.ButtonCancelText" @click.native="clickCancel()"/>
                <Button class="button-green" :buttonName="this.$resourceVn.ButtonApplyText"/>
            </div>
        </div>
    </div>
</template>
<script>

import fakeData from '../../js/fake-data/data-grid-add-component'

import Button from "../../components/base/button/BaseButton.vue"
import InputSearch from "../../components/base/input-search/BaseInputSearch.vue"
import Dropdown from "../../components/base/dropdown/BaseDropdown.vue"
import DataGrid from "../../components/base/data-grid/BaseDataGrid.vue"
import TheFooter from "../../components/layout/TheFooter.vue"

export default {
    name: "ModalBoxSalaryComponent",
    components: {
        InputSearch,
        Dropdown,
        DataGrid,
        TheFooter,
        Button,
    },
    props:{
        // Trạng thái ẩn hiện của modalbox
        modalBoxShow: {
            type: Boolean,
            default(){
                return false;
            }
        },
    },
    data() {
        return {
            listHeader: fakeData.listHeader,    //Dữ liệu các tiêu đề của grid
            dataSource: fakeData.dataSource,    //Dữ liệu của grid

        }
    },
    computed: {
        /**
         * @description Thay đổi trạng thái của popup
         * @date 18/09/2021
         * @createdBy LHTDung
         */
        modalBoxState() {
            if (this.modalBoxShow) {
                return "flex";
            } else {
                return "none";
            }
        },
    },

    methods: {
        clickCancel(){
            this.$emit("exitModalBox");        
        }
    },
}
</script>
<style scoped>
    @import "../../css/layout/modalbox-salary.css";
</style>