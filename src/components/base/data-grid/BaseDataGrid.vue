<template>
    <DxDataGrid
      ref="dataGrid"
      :data-source="dataSource"
      :remote-operations="false"
      :allow-column-reordering="true"
      :allow-column-resizing="true"
      :column-auto-width="true"
      :show-borders="true"
      @content-ready="onContentReady"
      :hover-state-enabled="true"
      :paging="paging"
      column-resizing-mode="widget"
      no-data-text="Không có dữ liệu"
      height="100%"
      width="100%"
      @selection-changed="onSelectionChanged"
      @row-click="showDetailForm"
    >
      <DxSelection
          :select-all-mode="allMode"
          :show-check-boxes-mode="checkBoxesMode"
          mode="multiple"
      />
      <DxColumn
        v-for="(item, index) in listHeader"
        :key="index"
        :data-field="item.DataField"
        :caption="item.Caption"
        :data-type="item.DataType"
        :width="item.Width"
        :fixed="item.Fixed ? item.Fixed : false"
        :alignment="item.Alignment ? item.Alignment : 'left'"
        cell-template="cell-template"
      />

      <DxColumn
          alignment="center"
          :width="130"
          class="option"
          :fixed="true"
          fixed-position="right"
          cell-template="option"
          v-if="modalBoxShow == false"
      />

      <template #cell-template="{ data }">
        <slot :name="data.column.dataField" :data="data.data"><div :title="data.data[data.column.dataField]">{{ data.data[data.column.dataField] }}</div></slot>
      </template>

      <template #option="{ data }">
          <div class="box-option">
              <div class="box-btn">
                  <div class="icon-20 url-background btn-stop-apply" title="Ngừng áp dụng" v-if="data.data.Status == 1" @click.stop="stopApply(data)"></div>
                  <div class="icon-20 url-background btn-active-apply" title="Đang áp dụng" v-if="data.data.Status == 2" @click.stop="activeApply(data)"></div>
                  <div class="icon-20 url-background btn-edit" title="Chỉnh sửa"></div>
                  <div class="icon-20 url-background btn-delete" title="Xóa" @click.stop="deletePolicy(data)"></div>
              </div>
          </div>
      </template>

      <DxGrouping :auto-expand-all="false"/>
      <DxScrolling column-rendering-mode="virtual"/>
    
    </DxDataGrid>
</template>
<script>
import {
  DxDataGrid,
  DxColumn,
  DxGrouping,
  DxScrolling,
  DxSelection,
} from 'devextreme-vue/data-grid';
import 'devextreme/data/odata/store';
import { EventBus } from "../../../main";
let collapsed = false;
export default {
  name: "DataGrid",
  components: {
    DxDataGrid,
    DxColumn,
    DxGrouping,
    DxScrolling,
    DxSelection,
  },
  props:{
    // Tiêu đề của bảng
    listHeader:{
      type: Array,
      default: () => {}
    },

    // Data của bảng
    dataSource: {
      type: Array,
      default: () => {}
    },

    // Trạng thái của modalbox
    modalBoxShow:{
      type: Boolean,
      default(){
        return false;
      }
    }

  },
  data() {
    return {
      onContentReady: function(e) {
        if (!collapsed) {
          e.component.expandRow(['EnviroCare']);
          collapsed = true;
        }
      },
      // Tắt paging default của DxDataGrid
      paging: {
          enabled:false,
      },
      allMode: 'page',
      checkBoxesMode: 'always',
    };
  },
  mounted() {
    /**
     * @description Bắt sự kiện Click "Bỏ chọn", bỏ tích tất cả các dòng trong DataGrid
     * @date 25/09/2021
     * @created LHTDung
     */
    EventBus.$on('clearSelectionDataGrid', () => {
        const dataGrid = this.$refs.dataGrid.instance;
        dataGrid.clearSelection();
    })
  },
  methods: {
      /**
       * @description Xử lý sự kiện thay đổi select trong data grid
       * @date 19/9/2021
       * @createdBy LHTDung
       */
      onSelectionChanged({ selectedRowsData }) {
          // Emit danh sách các dòng được checked lên SalaryPolicyList
          this.$emit('clickItemDataGrid', selectedRowsData);
          this.$emit('numberOfCheckedGrid', selectedRowsData);
      },

      /**
       * @description Xử lý sự kiện Click vào các row
       * @date 19/9/2021
       * @createdBy LHTDung
       */
      showDetailForm(e){
        // Emit object chứa dữ liệu của dòng được click 
        this.$emit('moveToViewScreen', e.key);
      },

      /**
       * @description Xử lý sự kiện click xóa từng dòng
       * @date 24/09/2021
       * @created LHTDung
       */
      deletePolicy(data){
        this.$emit('deleteSingleRow', data.data);
        EventBus.$emit('showPopupDeleteSingle', data.data.PolicyName, this.$resourceVn.POPUP_DELETE_TYPE);
      },

      /**
       * @description Xử lý sự kiện click Ngừng áp dụng
       * @date 24/09/2021
       * @created LHTDung
       */
      stopApply(data){
        EventBus.$emit('showPopupStopApply', data.data, this.$resourceVn.POPUP_STOPAPPLY_TYPE);
      },

      /**
       * @description Xử lý sự kiện click Đang áp dụng
       * @date 24/09/2021
       * @created LHTDung
       */
      activeApply(data){
        EventBus.$emit('showPopupActiveApply', data.data, this.$resourceVn.POPUP_ACTIVEAPPLY_TYPE);
      }
  },
};
</script>

<style scoped>
    @import "../../../css/base/data-grid/base-data-grid.css";
</style>

