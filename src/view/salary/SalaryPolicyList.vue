<template>
    <div class="content-area">
        <div class="content-title">
            <div class="title-row" v-if="mode == this.$resourceVn.MainScreen">
                <div class="title">Chính sách lương</div>
                <ButtonIcon :buttonName="this.$resourceVn.ButtonIconAddText" @click.native="clickAdd()"/>
            </div>
            <div class="title-row" v-if="mode == this.$resourceVn.AddScreen">
                <div class="wrap-title">
                    <div class="icon-36 icon-wrap" @click="clickCancel()">
                        <div class="icon-20 icon-title"></div>
                    </div>
                    <div class="title">Thêm chính sách lương</div>
                </div>
                <div class="wrap-button-group">
                    <Button class="button-white" :buttonName="this.$resourceVn.ButtonCancelText" @click.native="clickCancel()"/>
                    <Button class="button-green" :buttonName="this.$resourceVn.ButtonSaveText" @click.native="clickSave()"/>
                </div>
            </div>
            <!--------------------------- Xem chi tiết ---------------------->
            <div class="title-row" v-if="mode == this.$resourceVn.ViewScreen">
                <div class="wrap-title">
                    <div class="icon-36 icon-wrap" @click="clickCancel()">
                        <div class="icon-20 icon-title"></div>
                    </div>
                    <div class="title">Xem {{ objectEdit.PolicyName }}</div>
                </div>
                <div class="wrap-button-group">
                    <Button class="button-green" 
                    :buttonName="this.$resourceVn.ButtonEditText" 
                    @click.native="clickButtonEdit()"/>
                    <div class="icon-36 button-3-dots" @click="clickThreeDots()">
                        <div class="icon-20 icon-button-3-dots"></div>
                    </div>
                    <div class="wrap-duplicate-delete-popup" :class="{'show-popup' : clickedThreeDots}">
                        <div class="item-popup-3-dots">
                            <div class="icon-20 icon-duplicate"></div>
                            <div>{{ this.$resourceVn.DuplicateText }}</div>
                        </div>
                        <div class="item-popup-3-dots">
                            <div class="icon-20 icon-delete"></div>
                            <div>{{ this.$resourceVn.DeleteText }}</div>
                        </div>
                    </div>
                </div>
            </div>
            <!------------------------------ Sửa ---------------------------->
            <div class="title-row" v-if="mode == this.$resourceVn.EditScreen">
                <div class="wrap-title">
                    <div class="icon-36 icon-wrap" @click="clickCancel()">
                        <div class="icon-20 icon-title"></div>
                    </div>
                    <div class="title">Sửa {{ objectEdit.PolicyName }}</div>
                </div>
                <div class="wrap-button-group">
                    <Button class="button-white" :buttonName="this.$resourceVn.ButtonCancelText" @click.native="showPopup()"/>
                    <Button class="button-green" :buttonName="this.$resourceVn.ButtonSaveText"/>
                </div>
            </div>
        </div>
        <div class="responsive-content-wrap">
            <div class="responsive-content-main" :class="{'fix-width-content-main' : filterShow}">
                <div class="content-body"> 
                <div class="content-body-background">
                    <div class="content-body-tool">
                        <div class="content-tool-without-check" v-if="totalChecked == 0 || mode == this.$resourceVn.AddScreen ">
                            <div class="content-body-tool-left" v-if="mode == this.$resourceVn.MainScreen">
                                <InputSearch/>
                            </div>
                            <div class="content-body-tool-right" v-if="mode == this.$resourceVn.MainScreen">
                                <Dropdown width="230px" 
                                :listData="this.$resourceVn.Status" 
                                :typeDropdown="this.$resourceVn.DropdownStatus"/>
                                <DropdownSingle :treeDataSource="treeDataDepartment"/>
                                <div class="filter" @click="showFilterPopup()">
                                    <div class="icon-20 icon-filter"></div>
                                </div>
                                <div v-click-outside="hidePopupEditColumn">
                                    <div class="setting" @click="showCustomeColumn()">
                                    <div class="icon-24 icon-setting"></div>
                                    </div>
                                    <CustomeColumn :showPopupEditColumn="showPopupEditColumn"/>
                                </div>
                            </div>
                            <!-- THÔNG TIN CHUNG -->
                            <div class="general-info-wrap" v-if="mode == this.$resourceVn.AddScreen || mode == this.$resourceVn.ViewScreen || mode == this.$resourceVn.EditScreen">
                                <div class="general-info-title">
                                    {{ this.$resourceVn.GeneralInformation }}
                                </div>
                            </div>
                        </div>
                        <div class="content-tool-has-check" v-if="mode == this.$resourceVn.MainScreen && totalChecked > 0">
                            <div class="number-choosed">
                                <div>{{ this.$resourceVn.Choosed}} <span>{{totalChecked}}</span></div>
                            </div>
                            <div class="stop-apply">{{ this.$resourceVn.UnChecked }}</div>
                            <ButtonIcon class="button-icon-orange" 
                                :typeButton="this.$resourceVn.ButtonIconApply" 
                                :buttonName="this.$resourceVn.ButtonIconApplyText" 
                            />
                            <div class="space"></div>
                            <ButtonIcon class="button-icon-red" 
                                :typeButton="this.$resourceVn.ButtonIconDelete" 
                                :buttonName="this.$resourceVn.ButtonIconDeleteText" 
                            />
                        </div>
                    </div>
                </div>   
                </div>
                <div class="content-grid">
                    <DataGrid 
                    :listHeader="listHeader" 
                    :dataSource="dataSource"
                    @moveToViewScreen="moveToViewScreen($event)"
                    @numberOfCheckedGrid="numberOfCheckedGrid($event)"
                    v-if="mode == this.$resourceVn.MainScreen">
                        <template #Status="{ data }">
                            <div class="wrap-template-status">
                                <div class="dot-status" :class="{'dot-stop-apply' : data.StatusName == 'Ngừng áp dụng' ? true : false}"></div>
                                <div class="status-text" :class="{'stop-apply-text' : data.StatusName == 'Ngừng áp dụng' ? true : false}">{{ data.StatusName }}</div>
                            </div>
                        </template>
                    </DataGrid>
                    <div class="content-add-screen-wrap" v-if="mode == this.$resourceVn.AddScreen || mode == this.$resourceVn.ViewScreen || mode == this.$resourceVn.EditScreen">
                        <div class="row-flex">
                            <div class="label-name">{{ this.$resourceVn.DepartmentApply }}<span class="red-span"> *</span></div>
                            <!-- Hiển thị dropdown với màn thêm mới-->
                            <DropdownMultiple 
                            ref="dropdownMultipleRequired" 
                            v-model="objectEdit.DepartmentName"
                            :treeDataSource="treeDataDepartment"
                            v-if="mode == this.$resourceVn.AddScreen || mode == this.$resourceVn.EditScreen"
                            />
                            <!-- Hiển thị với màn xem chi tiết -->
                            <div class="view-object" v-if="mode == this.$resourceVn.ViewScreen">
                                {{ objectEdit.DepartmentName }}
                            </div>
                        </div>
                        <div class="validate-text" v-if="errorValidateDropdown">{{ this.$resourceVn.ValidateEmpty }}</div>
                        <div class="row-flex">
                            <div class="label-name">{{ this.$resourceVn.PositionApply }}</div>
                            <!-- Hiển thị combobox với màn thêm mới-->
                            <Combobox 
                            :placeholder="this.$resourceVn.AllPositionApply" 
                            :listObjectData="dataPosition"
                            :nameCombobox="this.$resourceVn.POSITION"
                            v-if="mode == this.$resourceVn.AddScreen || mode == this.$resourceVn.EditScreen"
                            />
                            <!-- Hiển thị với màn xem chi tiết -->
                            <div class="view-object" v-if="mode == this.$resourceVn.ViewScreen">
                                {{ objectEdit.PositionName }}
                            </div>
                        </div>
                        <div class="row-flex">
                            <div class="label-name">{{ this.$resourceVn.EmployeeApply }}</div>
                            <!-- Hiển thị combobox với màn thêm mới-->
                            <Combobox 
                            :placeholder="this.$resourceVn.AllEmployeeApply"
                            :listObjectData="dataEmployee" 
                            :nameCombobox="this.$resourceVn.EMPLOYEE"
                            v-if="mode == this.$resourceVn.AddScreen || mode == this.$resourceVn.EditScreen"
                            />
                            <!-- Hiển thị với màn xem chi tiết -->
                            <div class="view-object" v-if="mode == this.$resourceVn.ViewScreen">
                                {{ objectEdit.FullName }}
                            </div>
                        </div>
                        <div class="row-flex">
                            <div class="label-name">{{ this.$resourceVn.PolicyName }}<span class="red-span"> *</span></div>
                            <!-- Hiển thị input với màn thêm mới-->
                            <Input ref="inputRequired" 
                            @blur.native="checkValidate($event)" 
                            @focus.native="inputValidate($event)"
                            v-model="objectEdit.PolicyName"
                            v-if="mode == this.$resourceVn.AddScreen || mode == this.$resourceVn.EditScreen"
                            />
                            <!-- Hiển thị với màn xem chi tiết -->
                            <div class="view-object" v-if="mode == this.$resourceVn.ViewScreen">
                                {{ objectEdit.PolicyName }}
                            </div>
                        </div>
                        <div class="row-flex" v-if="mode == this.$resourceVn.ViewScreen || mode == this.$resourceVn.EditScreen">
                            <div class="label-name">{{ this.$resourceVn.StatusTextForm }}</div>
                            <div class="wrap-status">
                                <div class="wrap-radio-text">
                                    <div class="icon-32 radio-button">
                                        <div class="icon-20 radio-button-uncheck" 
                                        :class="{'radio-button-check' : objectEdit.StatusName == this.$resourceVn.Status[1].Text ? true: false}"></div>
                                    </div>
                                    <div>{{ this.$resourceVn.Status[1].Text }}</div>
                                </div>
                                <div class="wrap-radio-text">
                                    <div class="icon-32 radio-button">
                                        <div class="icon-20 radio-button-uncheck"
                                        :class="{'radio-button-check' : objectEdit.StatusName == this.$resourceVn.Status[2].Text ? true: false}"></div>
                                    </div>
                                    <div>{{ this.$resourceVn.Status[2].Text }}</div>
                                </div>
                            </div>
                        </div>

                        <div class="validate-text" v-if="errorValidateInput">{{ this.$resourceVn.ValidateEmpty }}</div>
                        <!----------------------------- THÀNH PHẦN LƯƠNG --------------------------------->
                        <div class="salary-component">
                            <div class="salary-component-text">{{ this.$resourceVn.SalaryComponent }}</div>
                        </div>
                        <div class="group-grid-salary-component" v-if="listChoosedComponent.length > 0">
                            <div class="grid-salary-component-title">
                                <div class="col-1">{{ this.$resourceVn.ComponentNameTitle }}</div>
                                <div class="col-2">{{ this.$resourceVn.ComponentCodeTitle }}</div>
                                <div class="col-3">{{ this.$resourceVn.FormulaTitle }}</div>
                            </div>
                            <draggable class="draggable" v-model="listChoosedComponent">
                                <transition-group>
                                    <div class="group-input-component" v-for="(item, index) in listChoosedComponent" :key="index">
                                        <div class="icon-20 icon-move"></div>
                                        <Input width="30%" class="noHover" 
                                        :value="item.ComponentName" 
                                        disabled :style="{background: '#E8EAEF'}"/>
                                        <div class="space"></div>
                                        <Input width="20%" class="noHover" 
                                        :value="item.ComponentCode" 
                                        disabled :style="{background: '#E8EAEF'}"/>
                                        <div class="space"></div>
                                        <Input width="50%" v-model="item.ComponentValue"/>
                                        <div class="icon-20 create-group"></div>
                                        <div class="icon-20 icon-delete-component" @click="deleteComponent(item)"></div>
                                    </div>
                                </transition-group>
                            </draggable>
                        </div>
                        <div class="component-button-add" v-if="mode == this.$resourceVn.AddScreen">
                            <ButtonIcon class="button-icon-white" 
                            :buttonName="this.$resourceVn.ButtonIconAddComponentText" 
                            @click.native="clickAddComponent()"/>
                        </div>
                    </div>
                </div>
                <TheFooter v-if="mode == this.$resourceVn.MainScreen"/>
            </div>
            <div class="responsive-filter">
                <FilterBox :filterShow="filterShow" @hideFilterPopup="hideFilterPopup"/>
            </div>
        </div>    
        <SalaryPolicyDetail :modalBoxShow="modalBoxShow" 
        :updateItemSource="updateItemSource"
        @getListComponent="getListComponent($event)"
        @exitModalBox="exitModalBox"/>
        <Popup @moveToMainScreen="moveToMainScreen"/>
    </div>
</template>
<script>
import fakeData from '../../js/fake-data/data-grid-main'

import axios from "axios";
import ClickOutSide from "../../js/detectoutside"
import { EventBus } from "../../main"
import draggable from 'vuedraggable'


import TheFooter from "../../components/layout/TheFooter.vue"
import ButtonIcon from "../../components/base/button-icon/BaseButtonIcon.vue"
import Button from "../../components/base/button/BaseButton.vue"
import DataGrid from "../../components/base/data-grid/BaseDataGrid.vue"
import DropdownSingle from "../../components/base/dropdown-single/BaseDropdownSingle.vue"
import Dropdown from '../../components/base/dropdown/BaseDropdown.vue'
import DropdownMultiple from "../../components/base/dropdown-multiple/BaseDropdownMultiple.vue"
import Combobox from '../../components/base/combobox/BaseCombobox.vue'
import Input from "../../components/base/input/BaseInput.vue"
import InputSearch from "../../components/base/input-search/BaseInputSearch.vue"
import SalaryPolicyDetail from "../salary/SalaryPolicyDetail.vue"
import CustomeColumn from "../../components/layout/CustomeColumn.vue"
import FilterBox from "../../components/base/filter/Filter.vue"
import Popup from "../../components/base/popup/BasePopup.vue"

export default {
    name: "SalaryPolicyList",
    directives: {
        ClickOutSide
    },
    components:{
        draggable,
        TheFooter,
        ButtonIcon,
        Button,
        DataGrid,
        DropdownSingle,
        Dropdown,
        DropdownMultiple,
        Combobox,
        Input,
        InputSearch,
        SalaryPolicyDetail,
        CustomeColumn,
        FilterBox,
        Popup
    },
    data(){
        return {
            modalBoxShow: false,                //Trạng thái ẩn/hiện của modalbox, mặc định ẩn
            showPopupEditColumn: false,         //Trạng thái ẩn/hiện của popup setting "Tùy chỉnh cột"
            filterShow: false,                  //Trạng thái ẩn/hiện của popup "Bộ lọc"
            errorValidateInput: false,               //Validate input
            errorValidateDropdown: false,            //Validate dropdown
            clickedThreeDots: false,            //Click button 3 dots trong màn xem chi tiết để thực hiện "Nhân bản, Xóa"

            mode: this.$resourceVn.MainScreen, //Màn hình hiển thị (0: Màn chính, 1: Màn Thêm chính sách lương)
            totalChecked: 0,                    //Tổng số dòng được checked trong Data Grid
            objectEdit: {},                     //Object chứa dữ liệu được click xem chi tiết 

            listHeader: fakeData.listHeader,    //Dữ liệu các tiêu đề của grid
            dataSource: [],                     //Dữ liệu của grid
            dataJSONParse: [],                  //Dữ liệu JSON Parse của grid
            treeDataDepartment: [],                 //Data cho Dropdown Single (Đơn vị)
            dataPosition: [],               //Data cho Combobox Tag (Vị trí)
            dataEmployee: [],               //Data cho Combobox Tag (Nhân viên)
            updateItemSource: {},           //Object chứa item "Thành phần lương" sau khi xóa (Cập nhật vào Data Grid vào bảng Thành phần)
            listChoosedComponent: [],       //Danh sách chứa các item "Thành phần Lương" sẵn sàng cho thêm mới
        }
    },
    mounted() {
      /************************************************ Gọi API ************************************************/
        /**
         * @description Gọi API lấy tất cả dữ liệu của Đơn vị
         * @created LHTDung
         * @date 22/09/2021
         */
        axios.get(this.$resourceVn.URL_DEPARTMENT_GETALL)
        .then(res => {
          this.treeDataDepartment = res.data;
        })
        .catch(err => {
          console.error(err); 
        })

        /**
         * @description Gọi API lấy tất cả dữ liệu của Vị trí
         * @created LHTDung
         * @date 22/09/2021
         */
        axios.get(this.$resourceVn.URL_POSITION_GETALL)
        .then(res => {
          this.dataPosition = res.data;
        })
        .catch(err => {
          console.error(err); 
        })

        /**
         * @description Gọi API lấy tất cả dữ liệu của Nhân viên
         * @created LHTDung
         * @date 22/09/2021
         */
        axios.get(this.$resourceVn.URL_EMPLOYEE_GETALL)
        .then(res => {
          this.dataEmployee = res.data;
        })
        .catch(err => {
          console.error(err); 
        })
        
        /**
         * @description Gọi API lấy tất cả dữ liệu của Chính sách
         * @created LHTDung
         * @date 22/09/2021
         */
        axios.get(this.$resourceVn.URL_POLICY_GETALL)
        .then(res => {
            this.dataSource = res.data;
        })
        .catch(err => {
            console.error(err); 
        })

    },
    methods: {
        /**
         * @description Xử lý sự kiện click button "Thêm mới"
         * @date 17/9/2021
         * @createdBy LHTDung
         */
        clickAdd(){
            this.mode = this.$resourceVn.AddScreen;
        },

        /**
         * @description Xử lý sự kiện click button "Hủy bỏ" hoặc trở lại
         * @date 17/9/2021
         * @createdBy LHTDung
         */
        clickCancel(){
            this.mode = this.$resourceVn.MainScreen;
        },

        /**
         * @description Xử lý sự kiện click button "Thêm thành phần"
         * @date 18/9/2021
         * @createdBy LHTDung
         */
        clickAddComponent(){
            //Hiển thị modalbox 
            this.modalBoxShow = true;
        },

        /**
         * @description Ẩn modalbox
         * @date 18/9/2021
         * @createdBy LHTDung
         */
        exitModalBox(){
            this.modalBoxShow = false;
        },

        /**
         * @description Xử lý sự kiện click button Setting, ẩn/hiện popup "Tùy chỉnh cột"
         * @date 18/9/2021
         * @createdBy LHTDung
         */
        showCustomeColumn(){
            this.showPopupEditColumn = !this.showPopupEditColumn;
        },

        /**
         * @description Ẩn popup "Tùy chỉnh cột" khi bị click ngoài phần tử
         * @date 18/9/2021
         * @createdBy LHTDung
         */
        hidePopupEditColumn(){
            this.showPopupEditColumn = false;
        },

        /**
         * @description Hiện popup "Bộ lọc" khi click button filter
         * @date 19/9/2021
         * @createdBy LHTDung
         */
        showFilterPopup(){
            this.filterShow = !this.filterShow;
        },

        /**
         * @description Đóng "Bộ lọc" khi click button exit
         * @date 19/9/2021
         * @createdBy LHTDung
         */
        hideFilterPopup(){
            this.filterShow = false;
        },
        
        /**
         * @description Nhận $emit danh sách được check từ Data Grid -> Lấy tổng số dòng được check
         * @date 20/9/2021
         * @createdBy LHTDung
         */
        numberOfCheckedGrid(listChecked){
            this.totalChecked = listChecked.length;
            console.log(this.totalChecked);
        },

        /**
         * @description Validate Input trong form thêm mới
         * @date 20/9/2021
         * @createdBy LHTDung
         */
        checkValidate(e){
            if(e.target.value == ""){
                this.errorValidateInput = true;
                this.$refs.inputRequired.$el.style.border = this.$resourceVn.BorderValidate;
            }else{
                this.errorValidateInput = false;
                this.$refs.inputRequired.$el.style.border = this.$resourceVn.BorderNormal;
            }
        },
        
        /**
         * @description Focus Input thay đổi border và ẩn validate
         * @date 20/9/2021
         * @createdBy LHTDung
         */
        inputValidate(e){
            console.log(e);
            this.errorValidateInput = false;
            this.$refs.inputRequired.$el.style.border = this.$resourceVn.BorderActive;
        },

        /**
         * @description Focus vào trường đầu tiên trong form
         * @date 20/9/2021
         * @createdBy LHTDung
         */
        focusFirstField(){
            this.$refs.dropdownMultipleRequired.$el.children[0].classList.add(this.$resourceVn.ClassFocus);
        },

        /*********************************** LƯU - THÊM MỚI *********************************/
        /**
         * @description Xử lý sự kiện click button "Lưu"
         * @date 20/9/2021
         * @createdBy LHTDung
         */
        clickSave(){
            //Hiển thị validate và border cho trường dropdown multiple "Đơn vị áp dụng"
            if(this.$refs.dropdownMultipleRequired.$el.children[0].classList.contains(this.$resourceVn.ClassEmpty)){
                this.errorValidateDropdown = true;
                this.$refs.dropdownMultipleRequired.$el.children[0].classList.add(this.$resourceVn.BorderErrorValidate);
            }else{
                this.errorValidateDropdown = false;
                this.$refs.dropdownMultipleRequired.$el.children[0].classList.remove(this.$resourceVn.BorderErrorValidate);
            }

            //Hiển thị validate và border cho trường input "Tên chính sách"
            if(this.$refs.inputRequired.$el.value == ""){
                this.errorValidateInput = true;
                this.$refs.inputRequired.$el.style.border = this.$resourceVn.BorderValidate;
            }

        },

        /**
         * @description Xử lý sự kiện click vào từng dòng của data grid -> Xem chi tiết
         * @date 20/9/2021
         * @createdBy LHTDung
         */
        moveToViewScreen(e){
            console.log(e);
            this.objectEdit = e;
            this.mode = this.$resourceVn.ViewScreen;
        },

        /**
         * @description Xử lý sự kiện click vào button 3 chấm -> Hiển thị Nhân bản/Xóa
         * @date 20/9/2021
         * @createdBy LHTDung
         */
        clickThreeDots(){
            this.clickedThreeDots = !this.clickedThreeDots;
        },

        /**
         * @description Xử lý sự kiện click vào button "Sửa" -> Bind dữ liệu vào form
         * @date 20/9/2021
         * @createdBy LHTDung
         */
        clickButtonEdit(){
            this.mode = this.$resourceVn.EditScreen;
        },

        /**
         * @description Hiển thị popup cảnh báo khi muốn hủy form đang chỉnh chỉnh sửa
         * @date 21/9/2021
         * @createdBy LHTDung
         */
        showPopup(){
            EventBus.$emit('showPopupCancelEditForm');
        },

        /**
         * @description Nhận emit từ popup và chuyển về màn chính, (main screen)
         * @date 21/9/2021
         * @createdBy LHTDung
         */
        moveToMainScreen(){
            this.mode = this.$resourceVn.MainScreen;
        },
        /***************************** THÀNH PHẦN LƯƠNG ************************/
        /**
         * @description Nhận danh sách "Thành phần lương" được chọn từ Datagrid
         * @date 21/9/2021
         * @createdBy LHTDung
         */
        getListComponent(e){
            this.listChoosedComponent = e;
            console.log(this.listChoosedComponent);
        },

        /**
         * @description Click button "Xóa" thành phần lương tại Thêm chính sách
         * @date 21/9/2021
         * @update 22/9/2021
         * @createdBy LHTDung
         */
        deleteComponent(value){
            this.updateItemSource = value;
            let index = this.listChoosedComponent.indexOf(value);
            if(index > - 1){
                this.listChoosedComponent.splice(index, 1);
            }
            console.log(value);
        }
    },

    watch:{
        mode: function(){
            //Thay đổi mode, khởi tạo lại các biển (validate)
            this.errorValidateInput = false;
            this.errorValidateDropdown = false;
            this.listChoosedComponent = [];

            //Focus vào trường đầu tiên trong form 
            if(this.mode == this.$resourceVn.AddScreen){
                this.$nextTick(() =>  {
                    this.focusFirstField();
                })
            }

        }
    }
}
</script>
<style scoped>
    @import "../../css/layout/content.css";
    @import "../../css/view/salary/SalaryPolicyList.css";
    @import "../../css/base/validate-text/validate-text.css";
</style>