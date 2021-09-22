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
                        :dataSource="dataSourceWithoutCheck" 
                        :modalBoxShow="modalBoxShow"
                        @clickItemDataGrid="clickItemDataGrid($event)"
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
                <Button class="button-green" :buttonName="this.$resourceVn.ButtonApplyText" @click.native="clickApply()"/>
            </div>
        </div>
    </div>
</template>
<script>

import fakeData from '../../js/fake-data/data-grid-add-component'

import axios from "axios";

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

        //Item thành phần sau khi xóa sẽ được cập nhật lại vào dataSourceWithoutCheck
        updateItemSource:{
            type: Object,
            default(){
                return {};
            }
        }
    },
    data() {
        return {
            listHeader: fakeData.listHeader,                //Dữ liệu các tiêu đề của grid Thành phần
            dataSource: [],                                 //Dữ liệu của grid Thành phần
            listItem: [],                                   //Danh sách chứa dữ liệu được chọn từ Datagrid Thành phần
            dataSourceWithoutCheck: this.dataSource,        //Dữ liệu chứa các bản ghi chưa được chọn trong bảng Thành phần (dùng để bind lên Table)
        }
    },
    mounted() {
        /************************************************ Gọi API ************************************************/
        axios.get(this.$resourceVn.URL_COMPONENT_GETALL)
        .then(res => {
            this.dataSource = res.data;
            this.dataSourceWithoutCheck = this.dataSource;
        })
        .catch(err => {
            console.error(err); 
        })
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
        /**
         * @description Xử lý sự kiện click button "Hủy bỏ"
         * @date 21/9/2021
         * @createdBy LHTDung
         */
        clickCancel(){
            this.$emit("exitModalBox");        
        },

        /**
         * @description Bắt sự kiện emit từ DataGrid, nhận dữ liệu các dòng được click
         * @date 21/9/2021
         * @createdBy LHTDung
         */
        clickItemDataGrid(listData){
            this.listItem = listData;
        },

        /**
         * @description Emit danh sách các thành phần được chọn đến SalaryPolicyList để hiển thị ra màn thêm mới
         * @date 21/9/2021
         * @createdBy LHTDung
         */
        clickApply(){
            this.listItem.forEach(itemList => {
                let index = this.dataSourceWithoutCheck.indexOf(itemList);
                if(index > - 1){
                    this.dataSourceWithoutCheck.splice(index, 1);
                }
            });
            console.log(this.dataSource);
            this.$emit('getListComponent', this.listItem);
            this.$emit("exitModalBox"); 
            console.log(this.listItem);       
        }
    },

    watch:{
        /**
         * @description Khi có sự thay đổi của updateItemSource (có Thành phần Lương được xóa) -> Update item vào danh sách dataSourceWithoutCheck
         * @date 22/9/2021
         * @created LHTDung
         */
        updateItemSource: function(){
            this.dataSourceWithoutCheck.unshift(this.updateItemSource);
        }
    }
}
</script>
<style scoped>
    @import "../../css/layout/modalbox-salary.css";
</style>